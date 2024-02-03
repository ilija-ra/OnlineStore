using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using OnlineStore.Communication.UserManagement;
using OnlineStore.Communication.UserManagement.Models;
using System.Fabric;

namespace OnlineStore.UserManagement
{
    internal sealed class UserManagement : StatelessService, IUserManagement
    {
        public UserManagement(StatelessServiceContext context)
            : base(context)
        { }

        #region IUserManagementImplementation

        public async Task<UserManagementUserGetByIdResponseModel> UserGetById(string? userId)
        {
            return new UserManagementUserGetByIdResponseModel()
            {
                Id = userId,
                FirstName = "Mark",
                LastName = "Johnson",
                FullName = "Mark Johnson",
                DateOfBirth = DateTime.UtcNow,
                Age = 35,
                Address = "40 Elmwood Ave",
                City = "New York",
                ZipCode = "11784"
            };
        }

        public async Task<UserManagementUserUpdateResponseModel> UserUpdate(UserManagementUserUpdateRequestModel? userModel)
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
                new UserManagementPurchaseGetAllItemModel(){ Name = "Purchase1" },
                new UserManagementPurchaseGetAllItemModel(){ Name = "Purchase2" },
                new UserManagementPurchaseGetAllItemModel(){ Name = "Purchase3" }
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
