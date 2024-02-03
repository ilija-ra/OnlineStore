using Microsoft.ServiceFabric.Services.Remoting;
using OnlineStore.Communication.UserManagement.Models;

namespace OnlineStore.Communication.UserManagement
{
    public interface IUserManagement : IService
    {
        Task<UserManagementUserGetByIdResponseModel> UserGetById(string? userId);

        Task<UserManagementUserUpdateResponseModel> UserUpdate(UserManagementUserUpdateRequestModel? userModel);

        Task<UserManagementPurchaseGetAllResponseModel> PurchaseGetAll(string? userId);
    }
}
