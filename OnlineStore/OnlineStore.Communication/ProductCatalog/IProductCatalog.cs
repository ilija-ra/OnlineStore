using Microsoft.ServiceFabric.Services.Remoting;
using OnlineStore.Communication.ProductCatalog.Models;

namespace OnlineStore.Communication.ProductCatalog
{
    public interface IProductCatalog : IService
    {
        Task<ProductCatalogProductSearchResponseModel> Search(ProductCatalogProductSearchRequestModel? model);

        Task<ProductCatalogProductGetByIdResponseModel> GetById(long? productId);
    }
}
