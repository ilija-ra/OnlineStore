using Microsoft.ServiceFabric.Services.Remoting;
using OnlineStore.Communication.ProductCatalog.Models;

namespace OnlineStore.Communication.ProductCatalog
{
    public interface IProductCatalog : IService
    {
        Task<ProductCatalogProductSearchResponseModel> Search(string? query);
    }
}
