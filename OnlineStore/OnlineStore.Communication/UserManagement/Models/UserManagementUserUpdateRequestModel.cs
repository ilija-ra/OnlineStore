namespace OnlineStore.Communication.UserManagement.Models
{
    public class UserManagementUserUpdateRequestModel
    {
        public string? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName => $"{this.FirstName} {this.LastName}";

        public string? Username { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public long? Age => this.DateOfBirth.HasValue ? Convert.ToInt32((DateTime.Now - this.DateOfBirth).Value.Days / 365) : null;

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? ZipCode { get; set; }
    }
}
