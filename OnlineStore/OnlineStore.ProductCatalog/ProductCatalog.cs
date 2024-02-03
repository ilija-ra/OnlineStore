using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using OnlineStore.Communication.ProductCatalog;
using System.Fabric;

namespace OnlineStore.ProductCatalog
{
    internal sealed class ProductCatalog : StatelessService, IProductCatalog
    {
        public ProductCatalog(StatelessServiceContext context)
            : base(context)
        { }

        #region IProductCatalogImplementation

        public async Task<string> Search(string? query)
        {
            return "The product catalog was searched!";
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

                ServiceEventSource.Current.ServiceMessage(this.Context, $"Working-{++iterations} :---: ReplicaId-{this.Context.InstanceId}");

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
