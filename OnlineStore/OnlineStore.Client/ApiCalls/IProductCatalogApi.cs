using OnlineStore.Client.Models.CatalogProduct;
using Refit;

namespace OnlineStore.Client.ApiCalls
{
    public interface IProductCatalogApi
    {
        [Post("/Search/{query}")]
        Task<CatalogProductListViewModel> Search(string query);
    }
}
