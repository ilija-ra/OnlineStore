using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Client.Models.UserManagement
{
    public class UserProfileDataModel
    {
        public string? Id { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        public string? Username { get; set; }

        [PasswordPropertyText]
        public string? Password { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        public long? Age { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }
    }
}
