namespace OnlineStore.Communication.UserManagement.Models
{
    public class UserManagementUserGetByIdResponseModel
    {
        public string? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? Age { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? ZipCode { get; set; }
    }
}
