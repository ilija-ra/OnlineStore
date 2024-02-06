namespace OnlineStore.Communication.ShoppingCart.Models
{
    public class ShoppingCartProductRemoveRequestModel
    {
        public long? ProductId { get; set; }

        public long? Quantity { get; set; }
    }
}
