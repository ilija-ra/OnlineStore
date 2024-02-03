using Microsoft.ServiceFabric.Services.Remoting;

namespace OnlineStore.Communication.ProductCatalog
{
    public interface IProductCatalog : IService
    {
        Task<string> Search(string? query);
    }
}
