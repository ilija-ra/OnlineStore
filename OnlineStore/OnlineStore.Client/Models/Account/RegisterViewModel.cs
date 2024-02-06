using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Client.Models.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [MinLength(6, ErrorMessage = "Username need to have at least 6 characters")]
        [MaxLength(6, ErrorMessage = "Username need to have 10 characters at max")]
        public string? Username { get; set; }

        [PasswordPropertyText]
        public string? Password { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }
    }
}
