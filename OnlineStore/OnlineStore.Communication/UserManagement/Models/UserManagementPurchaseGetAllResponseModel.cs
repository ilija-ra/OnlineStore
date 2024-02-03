namespace OnlineStore.Communication.UserManagement.Models
{
    public class UserManagementPurchaseGetAllResponseModel
    {
        public ICollection<UserManagementPurchaseGetAllItemModel>? Items { get; set; } = new List<UserManagementPurchaseGetAllItemModel>();
    }

    public class UserManagementPurchaseGetAllItemModel
    {
        public string? Name { get; set; }
    }
}
