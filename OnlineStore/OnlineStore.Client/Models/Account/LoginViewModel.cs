using System.ComponentModel;

namespace OnlineStore.Client.Models.Account
{
    public class LoginViewModel
    {
        public string? Username { get; set; }

        [PasswordPropertyText]
        public string? Password { get; set; }
    }
}
