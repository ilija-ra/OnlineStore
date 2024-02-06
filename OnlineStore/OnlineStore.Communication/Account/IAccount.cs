using Microsoft.ServiceFabric.Services.Remoting;
using OnlineStore.Communication.Account.Models;

namespace OnlineStore.Communication.Account
{
    public interface IAccount : IService
    {
        Task<AccountLoginResponseModel> Login(AccountLoginRequestModel? model);

        Task<AccountRegisterResponseModel> Register(AccountRegisterRequestModel? model);
    }
}
