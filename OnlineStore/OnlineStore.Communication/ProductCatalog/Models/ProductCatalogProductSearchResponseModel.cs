namespace OnlineStore.Communication.ProductCatalog.Models
{
    public class ProductCatalogProductSearchResponseModel
    {
        public ICollection<ProductCatalogProductSearchItemModel>? Items { get; set; } = new List<ProductCatalogProductSearchItemModel>();
    }

    public class ProductCatalogProductSearchItemModel
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double? Price { get; set; }

        public long? Quantity { get; set; }

        public string? Category { get; set; }
    }
}
