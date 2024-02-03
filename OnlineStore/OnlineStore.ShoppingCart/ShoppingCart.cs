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
        public ShoppingCart(StatefulServiceContext context)
            : base(context)
        { }

        #region IShoppingCartImplementation

        public async Task<ShoppingCartProductAddResponseModel> Add(ShoppingCartProductAddRequestModel? model)
        {
            return new ShoppingCartProductAddResponseModel();
        }

        public async Task<ShoppingCartProductRemoveResponseModel> Remove(ShoppingCartProductRemoveRequestModel? model)
        {
            return new ShoppingCartProductRemoveResponseModel();
        }

        #endregion

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return this.CreateServiceRemotingReplicaListeners();
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {

            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("myDictionary");

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
