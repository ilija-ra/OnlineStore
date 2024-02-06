using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using OnlineStore.Communication.Account;
using OnlineStore.Communication.Account.Models;
using OnlineStore.Communication.UserManagement;
using OnlineStore.Communication.UserManagement.Models;
using System.Fabric;

namespace OnlineStore.UserManagement
{
    internal sealed class UserManagement : StatelessService, IUserManagement, IAccount
    {
        public UserManagement(StatelessServiceContext context)
            : base(context)
        { }

        #region IAccountImplementation

        public async Task<AccountLoginResponseModel> Login(AccountLoginRequestModel? model)
        {
            if (model is null)
            {
                return null!;
            }

            var result = await this.UserGetByCredentials(model.Username, model.Password);

            if (result is null)
            {
                return null!;
            }

            return new AccountLoginResponseModel()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Username = result.Username,
                Password = result.Password,
                DateOfBirth = result.DateOfBirth,
                Age = result.Age,
                Address = result.Address,
                City = result.City,
                ZipCode = result.ZipCode,
                IsAuthenticated = true
            };
        }

        public async Task<AccountRegisterResponseModel> Register(AccountRegisterRequestModel? model)
        {
            return new AccountRegisterResponseModel();
        }

        #endregion

        #region IUserManagementImplementation

        public async Task<UserManagementUserGetByIdResponseModel> UserGetById(string? userId)
        {
            return new UserManagementUserGetByIdResponseModel()
            {
                Id = userId,
                FirstName = "Mark",
                LastName = "Johnson",
                Username = "marky",
                Password = "marky123",
                DateOfBirth = DateTime.UtcNow,
                Age = 35,
                Address = "40 Elmwood Ave",
                City = "New York",
                ZipCode = "11784"
            };
        }

        public async Task<UserManagementUserGetByCredentialsResponseModel> UserGetByCredentials(string? username, string? password)
        {
            var userList = new List<UserManagementUserGetByCredentialsResponseModel>
            {
                new UserManagementUserGetByCredentialsResponseModel
                {
                    Id = "1",
                    FirstName = "John",
                    LastName = "Doe",
                    Username = "john_doe",
                    Password = "password123",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    Age = 32,
                    Address = "123 Main St",
                    City = "Example City",
                    ZipCode = "12345"
                },
                new UserManagementUserGetByCredentialsResponseModel
                {
                    Id = "2",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Username = "jane_smith",
                    Password = "securepwd456",
                    DateOfBirth = new DateTime(1985, 8, 25),
                    Age = 37,
                    Address = "456 Oak Ave",
                    City = "Another City",
                    ZipCode = "67890"
                },
                new UserManagementUserGetByCredentialsResponseModel
                {
                    Id = "3",
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Username = "alice_j",
                    Password = "pass123",
                    DateOfBirth = new DateTime(1995, 11, 10),
                    Age = 27,
                    Address = "789 Pine Rd",
                    City = "Yet Another City",
                    ZipCode = "54321"
                }
            };

            return userList.Where(x => x.Username == username && x.Password == password).FirstOrDefault()!;
        }

        public async Task<UserManagementUserUpdateResponseModel> UserUpdate(UserManagementUserUpdateRequestModel? model)
        {
            //{
            //    "id": "555",
            //    "firstName": "Mark",
            //    "lastName": "Johnson",
            //    "dateOfBirth": "1986-02-03T03:44:32.295Z",
            //    "address": "40 Elmwood Ave",
            //    "city": "New York",
            //    "zipCode": "11784"
            //}
            return new UserManagementUserUpdateResponseModel();
        }

        public async Task<UserManagementPurchaseGetAllResponseModel> PurchaseGetAll(string? userId)
        {
            var response = new UserManagementPurchaseGetAllResponseModel();

            response.Items = new List<UserManagementPurchaseGetAllItemModel>()
            {
                new UserManagementPurchaseGetAllItemModel(){ Id = 1, PurchaseDate = DateTime.Now, PurchasedProducts = new List<UserManagementPurchaseProductItemModel>(), TotalAmount = 1500, PaymentMethod = "PayPal", UserId = "111" },
                new UserManagementPurchaseGetAllItemModel(){ Id = 2, PurchaseDate = DateTime.Now, PurchasedProducts = new List<UserManagementPurchaseProductItemModel>(), TotalAmount = 1200, PaymentMethod = "Cash On Delivery", UserId = "111" },
                new UserManagementPurchaseGetAllItemModel(){ Id = 3, PurchaseDate = DateTime.Now, PurchasedProducts = new List<UserManagementPurchaseProductItemModel>(), TotalAmount = 1700, PaymentMethod = "Cash On Delivery", UserId = "111" }
            };

            return response;
        }

        #endregion

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners();
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            long iterations = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
