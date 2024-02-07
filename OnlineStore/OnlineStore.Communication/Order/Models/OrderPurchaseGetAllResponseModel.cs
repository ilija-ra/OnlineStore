namespace OnlineStore.Communication.Order.Models
{
    public class OrderPurchaseGetAllResponseModel
    {
        public ICollection<OrderPurchaseGetAllItemModel>? Items { get; set; } = new List<OrderPurchaseGetAllItemModel>();
    }

    public class OrderPurchaseGetAllItemModel
    {
        public long? Id { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public string? PurchasedProducts { get; set; }

        public double? TotalAmount { get; set; }

        public string? CardNumber { get; set; }

        public string? PaymentMethod { get; set; }

        public string? UserId { get; set; }
    }
}
