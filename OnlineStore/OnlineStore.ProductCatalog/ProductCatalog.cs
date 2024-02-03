using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using OnlineStore.Communication.ProductCatalog;
using OnlineStore.Communication.ProductCatalog.Models;
using System.Fabric;

namespace OnlineStore.ProductCatalog
{
    internal sealed class ProductCatalog : StatelessService, IProductCatalog
    {
        public ProductCatalog(StatelessServiceContext context)
            : base(context)
        { }

        #region IProductCatalogImplementation

        public async Task<ProductCatalogProductSearchResponseModel> Search(string? query)
        {
            return new ProductCatalogProductSearchResponseModel();
        }

        public async Task<ProductCatalogProductGetByIdResponseModel> GetById(long? productId)
        {
            throw new NotImplementedException();
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
