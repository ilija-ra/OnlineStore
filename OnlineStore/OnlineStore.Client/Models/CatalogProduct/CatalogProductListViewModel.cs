namespace OnlineStore.Client.Models.CatalogProduct
{
    public class CatalogProductListViewModel
    {
        public ICollection<CatalogProductViewModel>? Items { get; set; } = new List<CatalogProductViewModel>();
    }
}
