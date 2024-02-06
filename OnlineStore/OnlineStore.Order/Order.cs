using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using OnlineStore.Communication.Order;
using OnlineStore.Communication.Order.Models;
using System.Fabric;

namespace OnlineStore.Order
{
    internal sealed class Order : StatelessService, IOrder
    {
        public Order(StatelessServiceContext context)
            : base(context)
        { }

        #region IOrderImplementation

        public async Task<OrderPurchaseConfirmResponseModel> PurchaseConfirm(OrderPurchaseConfirmRequestModel model)
        {
            //var brokerClient = new BrokerClient();
            //brokerClient.PublishMessageAsync(new PublishedMessageOne { Content = "Hello PubSub World!" });

            return new OrderPurchaseConfirmResponseModel();
        }

        public async Task<OrderPurchaseGetAllResponseModel> PurchaseGetAll(string? userId)
        {
            var response = new OrderPurchaseGetAllResponseModel();

            response.Items = new List<OrderPurchaseGetAllItemModel>()
            {
                new OrderPurchaseGetAllItemModel(){ Id = 1, PurchaseDate = DateTime.Now, PurchasedProducts = new List<string>(){ "Banana", "Apple", "Milk" }, TotalAmount = 1500, PaymentMethod = "PayPal", UserId = "111" },
                new OrderPurchaseGetAllItemModel(){ Id = 2, PurchaseDate = DateTime.Now, PurchasedProducts = new List<string>(){ "Cheese", "Snickers", "Twix" }, TotalAmount = 1200, PaymentMethod = "Cash On Delivery", UserId = "111" },
                new OrderPurchaseGetAllItemModel(){ Id = 3, PurchaseDate = DateTime.Now, PurchasedProducts = new List<string>(){ "Strawberry", "Berry" }, TotalAmount = 1700, PaymentMethod = "Cash On Delivery", UserId = "111" }
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
