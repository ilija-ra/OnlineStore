using Microsoft.ServiceFabric.Services.Remoting;
using OnlineStore.Communication.Order.Models;

namespace OnlineStore.Communication.Order
{
    public interface IOrder : IService
    {
        Task<OrderConfirmResponseModel> Confirm(OrderConfirmRequestModel model);
    }
}
