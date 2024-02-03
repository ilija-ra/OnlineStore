namespace OnlineStore.Communication.ShoppingCart.Models
{
    public class ShoppingCartProductGetAllResponseModel
    {
        public ICollection<ShoppingCartProductGetAllItemModel>? Items { get; set; } = new List<ShoppingCartProductGetAllItemModel>();
    }

    public class ShoppingCartProductGetAllItemModel
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double? Price { get; set; }

        public long? Quantity { get; set; }

        public string? Category { get; set; }

        public string? UserId { get; set; }
    }
}
