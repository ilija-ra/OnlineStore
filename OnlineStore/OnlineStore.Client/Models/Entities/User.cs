using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Client.Models.Entities
{
    public class User
    {
        [Key]
        public string? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public long? Age { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? ZipCode { get; set; }
    }
}
