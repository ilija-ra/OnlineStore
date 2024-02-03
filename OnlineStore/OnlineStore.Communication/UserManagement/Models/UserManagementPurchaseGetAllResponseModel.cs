namespace OnlineStore.Communication.UserManagement.Models
{
    public class UserManagementPurchaseGetAllResponseModel
    {
        public ICollection<UserManagementPurchaseGetAllItemModel>? Items { get; set; } = new List<UserManagementPurchaseGetAllItemModel>();
    }

    public class UserManagementPurchaseGetAllItemModel
    {
        public long? Id { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public List<UserManagementPurchaseProductItemModel>? PurchasedProducts { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? PaymentMethod { get; set; }

        public string? UserId { get; set; }
    }

    public class UserManagementPurchaseProductItemModel
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public double? Price { get; set; }

        public long? Quantity { get; set; }

        public string? Category { get; set; }
    }
}
