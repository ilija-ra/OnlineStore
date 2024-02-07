using Microsoft.AspNetCore.Identity;

namespace OnlineStore.Client.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string? FirstName { get; set; }

    [PersonalData]
    public string? LastName { get; set; }

    public string? FullName => $"{FirstName} {LastName}";

    [PersonalData]
    public DateTime? DateOfBirth { get; set; }

    [PersonalData]
    public long? Age => this.DateOfBirth.HasValue ? Convert.ToInt32((DateTime.Now - this.DateOfBirth).Value.Days / 365) : null;

    [PersonalData]
    public string? Address { get; set; }

    [PersonalData]
    public string? City { get; set; }

    [PersonalData]
    public string? ZipCode { get; set; }
}
