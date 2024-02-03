namespace OnlineStore.Communication.Order.Models
{
    public class OrderConfirmRequestModel
    {
        public long? Id { get; set; }

        public long? ProductId { get; set; }

        public string? UserId { get; set; }

        public DateTime? DateOfConfirmation { get; set; }

        public string? CardNumber { get; set; }

        public long? Quantity { get; set; }

        public string? PaymentMethod { get; set; }
    }
}
