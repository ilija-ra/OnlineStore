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

        public async Task<ProductCatalogProductSearchResponseModel> Search(ProductCatalogProductSearchRequestModel? model)
        {
            var response = new ProductCatalogProductSearchResponseModel();
            response.Items = new List<ProductCatalogProductSearchItemModel>
            {
                new ProductCatalogProductSearchItemModel
                {
                    Id = 1,
                    Name = "Product 1",
                    Description = "Description of Product 1",
                    Price = 19.99,
                    Quantity = 100,
                    Category = "Electronics"
                },
                new ProductCatalogProductSearchItemModel
                {
                    Id = 2,
                    Name = "Product 2",
                    Description = "Description of Product 2",
                    Price = 29.99,
                    Quantity = 50,
                    Category = "Clothing"
                },
                new ProductCatalogProductSearchItemModel
                {
                    Id = 3,
                    Name = "Product 3",
                    Description = "Description of Product 3",
                    Price = 39.99,
                    Quantity = 75,
                    Category = "Home Appliances"
                },
                new ProductCatalogProductSearchItemModel
                {
                    Id = 4,
                    Name = "Product 4",
                    Description = "Description of Product 4",
                    Price = 49.99,
                    Quantity = 120,
                    Category = "Furniture"
                }
            };

            if (model!.Query is null || model!.Query == string.Empty)
            {
                return response;
            }

            model!.Query = model!.Query?.ToLower();

            var filtered = response.Items.Where(x => x.Name!.ToLower().Contains(model!.Query!) ||
                                                     x.Description!.ToLower().Contains(model!.Query!) ||
                                                     x.Category!.ToLower().Contains(model!.Query!)).ToList();

            response.Items = filtered;

            return response;
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
