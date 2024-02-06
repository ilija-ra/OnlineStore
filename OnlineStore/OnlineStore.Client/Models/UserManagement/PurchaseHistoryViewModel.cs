using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Client.Models.UserManagement
{
    public class PurchaseHistoryViewModel
    {
        public long? Id { get; set; }

        [Display(Name = "Purchase Date")]
        public string? PurchaseDate { get; set; }

        [Display(Name = "Purchased Products")]
        public List<string>? PurchasedProducts { get; set; }

        [Display(Name = "Total Amount")]
        public decimal? TotalAmount { get; set; }

        [Display(Name = "Payment method")]
        public string? PaymentMethod { get; set; }

        public string? UserId { get; set; }
    }
}
