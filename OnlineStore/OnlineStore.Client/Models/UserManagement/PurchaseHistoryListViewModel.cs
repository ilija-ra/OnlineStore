namespace OnlineStore.Client.Models.UserManagement
{
    public class PurchaseHistoryListViewModel
    {
        public ICollection<PurchaseHistoryViewModel>? Items { get; set; } = new List<PurchaseHistoryViewModel>();
    }
}
