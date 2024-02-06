namespace OnlineStore.Client.Models.CatalogProduct
{
    public class ShoppingCartListViewModel
    {
        public ICollection<ShoppingCartViewModel>? Items { get; set; } = new List<ShoppingCartViewModel>();
    }
}
