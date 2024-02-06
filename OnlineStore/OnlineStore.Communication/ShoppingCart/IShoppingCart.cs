using Microsoft.ServiceFabric.Services.Remoting;
using OnlineStore.Communication.ShoppingCart.Models;

namespace OnlineStore.Communication.ShoppingCart
{
    public interface IShoppingCart : IService
    {
        Task<ShoppingCartProductAddResponseModel> Add(ShoppingCartProductAddRequestModel? model, string? userId);

        Task<ShoppingCartQuantityIncreaseResponseModel> IncreaseQuantity(long? productId, string? userId);

        Task<ShoppingCartProductRemoveResponseModel> Remove(ShoppingCartProductRemoveRequestModel? model, string? userId);

        Task<ShoppingCartQuantityDecreaseResponseModel> DecreaseQuantity(long? productId, string? userId);

        Task<ShoppingCartProductGetAllResponseModel> GetAll(string? userId);
    }
}
