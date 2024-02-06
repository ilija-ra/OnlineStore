namespace OnlineStore.Communication.UserManagement.Models
{
    public class UserManagementPurchaseGetAllResponseModel
    {
        public ICollection<UserManagementPurchaseGetAllItemModel>? Items { get; set; } = new List<UserManagementPurchaseGetAllItemModel>();
    }

    public class UserManagementPurchaseGetAllItemModel
    {
        public long? Id { get; set; }

        public string? PurchaseDate { get; set; }

        public List<string>? PurchasedProducts { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? PaymentMethod { get; set; }

        public string? UserId { get; set; }
    }
}
