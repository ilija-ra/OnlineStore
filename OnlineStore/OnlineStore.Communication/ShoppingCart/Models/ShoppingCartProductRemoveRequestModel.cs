namespace OnlineStore.Communication.ShoppingCart.Models
{
    public class ShoppingCartProductRemoveRequestModel
    {
        public string? CustomerId { get; set; }

        public long? ItemId { get; set; }

        public int? Quantity { get; set; }
    }
}
