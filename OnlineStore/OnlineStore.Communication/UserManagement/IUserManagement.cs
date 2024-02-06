using Microsoft.ServiceFabric.Services.Remoting;
using OnlineStore.Communication.UserManagement.Models;

namespace OnlineStore.Communication.UserManagement
{
    public interface IUserManagement : IService
    {
        Task<UserManagementUserGetByIdResponseModel> UserGetById(string? userId);

        Task<UserManagementUserGetByCredentialsResponseModel> UserGetByCredentials(string? username, string? password);

        Task<UserManagementUserUpdateResponseModel> UserUpdate(UserManagementUserUpdateRequestModel? model);

        Task<UserManagementPurchaseGetAllResponseModel> PurchaseGetAll(string? userId);
    }
}
