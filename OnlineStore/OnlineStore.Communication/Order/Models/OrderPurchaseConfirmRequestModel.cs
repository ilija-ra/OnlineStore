namespace OnlineStore.Communication.Order.Models
{
    public class OrderPurchaseConfirmRequestModel
    {
        public long? Id { get; set; }

        public string? Products { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public string? CardNumber { get; set; }

        public double? TotalAmount { get; set; }

        public string? PaymentMethod { get; set; }

        public string? UserId { get; set; }
    }
}
