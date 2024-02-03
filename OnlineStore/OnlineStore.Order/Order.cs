using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using OnlineStore.Communication.Order;
using OnlineStore.Communication.Order.Models;
using OnlineStore.Communication.PubSub;
using SoCreate.ServiceFabric.PubSub;
using System.Fabric;

namespace OnlineStore.Order
{
    internal sealed class Order : StatelessService, IOrder
    {
        public Order(StatelessServiceContext context)
            : base(context)
        { }

        #region IOrderImplementation

        public async Task<OrderConfirmResponseModel> Confirm(OrderConfirmRequestModel model)
        {
            var brokerClient = new BrokerClient();
            brokerClient.PublishMessageAsync(new PublishedMessageOne { Content = "Hello PubSub World!" });

            return new OrderConfirmResponseModel();
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
