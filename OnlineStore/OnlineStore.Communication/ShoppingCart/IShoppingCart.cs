using Microsoft.ServiceFabric.Services.Remoting;
using OnlineStore.Communication.ShoppingCart.Models;

namespace OnlineStore.Communication.ShoppingCart
{
    public interface IShoppingCart : IService
    {
        Task<ShoppingCartProductAddResponseModel> Add(ShoppingCartProductAddRequestModel? model);

        Task<ShoppingCartProductRemoveResponseModel> Remove(ShoppingCartProductRemoveRequestModel? model);
    }
}
