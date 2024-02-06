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

        public List<string>? PurchasedProducts { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? PaymentMethod { get; set; }

        public string? UserId { get; set; }
    }

    //public class OrderPurchaseProductItemModel
    //{
    //    public long? Id { get; set; }

    //    public string? Name { get; set; }

    //    public double? Price { get; set; }

    //    public long? Quantity { get; set; }

    //    public string? Category { get; set; }
    //}
}
