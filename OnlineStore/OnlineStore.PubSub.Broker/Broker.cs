using SoCreate.ServiceFabric.PubSub;
using System.Fabric;

namespace OnlineStore.PubSub.Broker
{
    internal sealed class Broker : BrokerService
    {
        public Broker(StatefulServiceContext context)
           : base(context)
        {
        }
    }
}
