namespace OnlineStore.Communication.Order.Models
{
    public class OrderPurchaseConfirmRequestModel
    {
        public long? Id { get; set; }

        public List<OrderPurchaseConfirmItemModel>? Products { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public string? CardNumber { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? PaymentMethod { get; set; }

        public string? UserId { get; set; }
    }

    public class OrderPurchaseConfirmItemModel
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double? Price { get; set; }

        public long? Quantity { get; set; }

        public string? Category { get; set; }
    }
}
