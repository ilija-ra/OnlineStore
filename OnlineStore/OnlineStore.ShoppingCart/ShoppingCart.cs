using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using OnlineStore.Communication.ShoppingCart;
using OnlineStore.Communication.ShoppingCart.Models;
using System.Fabric;

namespace OnlineStore.ShoppingCart
{
    internal sealed class ShoppingCart : StatefulService, IShoppingCart
    {
        private IReliableDictionary<long, ShoppingCartProductGetAllItemModel> _cartProducts;
        private readonly IReliableStateManager _stateManager;

        public ShoppingCart(StatefulServiceContext context) : base(context)
        {
            _stateManager = this.StateManager;
        }

        #region IShoppingCartImplementation

        public async Task<ShoppingCartProductAddResponseModel> Add(ShoppingCartProductAddRequestModel? model, string? userId)
        {
            using (ITransaction tx = _stateManager.CreateTransaction())
            {
                var convertedModel = new ShoppingCartProductGetAllItemModel()
                {
                    Id = model!.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Category = model.Category,
                    UserId = userId
                };

                ConditionalValue<ShoppingCartProductGetAllItemModel> existingProduct = await _cartProducts.TryGetValueAsync(tx, convertedModel!.Id!.Value);

                if (existingProduct.HasValue && existingProduct.Value.UserId == convertedModel.UserId)
                {
                    convertedModel.Quantity += existingProduct.Value.Quantity;
                }

                await _cartProducts.AddOrUpdateAsync(tx, convertedModel.Id!.Value, convertedModel, (id, value) => convertedModel);
                await tx.CommitAsync();
            }

            return new ShoppingCartProductAddResponseModel();
        }

        public async Task<ShoppingCartQuantityIncreaseResponseModel> IncreaseQuantity(long? productId, string? userId)
        {
            using (ITransaction tx = _stateManager.CreateTransaction())
            {
                ConditionalValue<ShoppingCartProductGetAllItemModel> existingProduct = await _cartProducts.TryGetValueAsync(tx, productId!.Value);

                if (existingProduct.HasValue && existingProduct.Value.UserId == userId)
                {
                    existingProduct.Value.Quantity += 1;
                }

                await _cartProducts.AddOrUpdateAsync(tx, existingProduct.Value.Id!.Value, existingProduct.Value, (id, value) => existingProduct.Value);
                await tx.CommitAsync();
            }

            return new ShoppingCartQuantityIncreaseResponseModel();
        }

        public async Task<ShoppingCartProductRemoveResponseModel> Remove(ShoppingCartProductRemoveRequestModel? model, string? userId)
        {
            using (ITransaction tx = _stateManager.CreateTransaction())
            {
                ConditionalValue<ShoppingCartProductGetAllItemModel> existingProduct = await _cartProducts.TryGetValueAsync(tx, model!.ProductId!.Value);

                if (existingProduct.HasValue && existingProduct.Value.UserId == userId)
                {
                    existingProduct.Value.Quantity -= model.Quantity;
                }

                await _cartProducts.AddOrUpdateAsync(tx, existingProduct.Value.Id!.Value, existingProduct.Value, (id, value) => existingProduct.Value);
                await tx.CommitAsync();
            }

            return new ShoppingCartProductRemoveResponseModel();
        }

        public async Task<ShoppingCartQuantityDecreaseResponseModel> DecreaseQuantity(long? productId, string? userId)
        {
            using (ITransaction tx = _stateManager.CreateTransaction())
            {
                ConditionalValue<ShoppingCartProductGetAllItemModel> existingProduct = await _cartProducts.TryGetValueAsync(tx, productId!.Value);

                if (existingProduct.HasValue && existingProduct.Value.UserId == userId)
                {
                    existingProduct.Value.Quantity -= 1;
                }

                await _cartProducts.AddOrUpdateAsync(tx, existingProduct.Value.Id!.Value, existingProduct.Value, (id, value) => existingProduct.Value);
                await tx.CommitAsync();
            }

            return new ShoppingCartQuantityDecreaseResponseModel();
        }

        public async Task<ShoppingCartProductGetAllResponseModel> GetAll(string? userId)
        {
            var response = new ShoppingCartProductGetAllResponseModel();

            using (var tx = _stateManager.CreateTransaction())
            {
                var enumerator = (await _cartProducts.CreateEnumerableAsync(tx)).GetAsyncEnumerator();

                while (await enumerator.MoveNextAsync(default))
                {
                    response.Items!.Add(enumerator.Current.Value);
                }
            }

            return response;
        }

        #endregion

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return this.CreateServiceRemotingReplicaListeners();
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {

            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("myDictionary");
            _cartProducts = await _stateManager.GetOrAddAsync<IReliableDictionary<long, ShoppingCartProductGetAllItemModel>>("cartProducts");

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var tx = this.StateManager.CreateTransaction())
                {
                    var result = await myDictionary.TryGetValueAsync(tx, "Counter");

                    ServiceEventSource.Current.ServiceMessage(this.Context, "Current Counter Value: {0}",
                        result.HasValue ? result.Value.ToString() : "Value does not exist.");

                    await myDictionary.AddOrUpdateAsync(tx, "Counter", 0, (key, value) => ++value);

                    await tx.CommitAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
