namespace OnlineStore.Communication.ShoppingCart.Models
{
    public class ShoppingCartProductAddRequestModel
    {
        public string? CustomerId { get; set; }

        public long? ItemId { get; set; }

        public int? Quantity { get; set; }
    }
}
