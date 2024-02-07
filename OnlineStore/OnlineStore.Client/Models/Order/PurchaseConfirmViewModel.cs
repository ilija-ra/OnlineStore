using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Client.Models.Order
{
    public class PurchaseConfirmViewModel
    {
        public long? Id { get; set; }

        public string? Products { get; set; }

        [Display(Name = "Purchase Date")]
        public DateTime? PurchaseDate { get; set; }

        [Display(Name = "Card Number")]
        public string? CardNumber { get; set; }

        [Display(Name = "Total Amount")]
        public double? TotalAmount { get; set; }

        [Display(Name = "Payment Method")]
        public string? PaymentMethod { get; set; }

        public string? UserId { get; set; }
    }
}
