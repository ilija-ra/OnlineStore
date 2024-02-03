using Microsoft.ServiceFabric.Services.Remoting;

namespace OnlineStore.Communication.ShoppingCart
{
    public interface IShoppingCart : IService
    {
        Task<string> Add(string? item);

        Task<string> Remove(string? itemId);
    }
}
