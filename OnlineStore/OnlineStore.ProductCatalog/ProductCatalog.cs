using Azure.Data.Tables;
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
        string azuriteAccountName = "devstoreaccount1";
        string azuriteAccountKey = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";
        Uri azuriteTableEndpoint = new Uri("http://127.0.0.1:10002/devstoreaccount1");

        public ProductCatalog(StatelessServiceContext context)
            : base(context)
        { }

        #region IProductCatalogImplementation

        public async Task<ProductCatalogProductSearchResponseModel> Search(ProductCatalogProductSearchRequestModel? model)
        {
            var tableServiceClient = new TableServiceClient(azuriteTableEndpoint, new TableSharedKeyCredential(azuriteAccountName, azuriteAccountKey));

            var containerClient = tableServiceClient.GetTableClient("Products");

            var products = containerClient.QueryAsync<TableEntity>();

            var response = new ProductCatalogProductSearchResponseModel();

            await foreach (var product in products)
            {
                response.Items!.Add(new ProductCatalogProductSearchItemModel()
                {
                    Id = Convert.ToInt64(product["RowKey"]),
                    Name = product["Name"].ToString(),
                    Description = product["Description"].ToString(),
                    Price = Convert.ToDouble(product["Price"]),
                    Quantity = Convert.ToInt64(product["Quantity"]),
                    Category = product["Category"].ToString()
                });
            }

            if (model!.Query is null || model!.Query == string.Empty)
            {
                return response;
            }

            model!.Query = model!.Query?.ToLower();

            var filtered = response.Items!.Where(x => x.Name!.ToLower().Contains(model!.Query!) ||
                                                      x.Description!.ToLower().Contains(model!.Query!) ||
                                                      x.Category!.ToLower().Contains(model!.Query!)).ToList();

            response.Items = filtered;

            return response;
        }

        public async Task<ProductCatalogProductGetByIdResponseModel> GetById(long? productId)
        {
            var tableServiceClient = new TableServiceClient(azuriteTableEndpoint, new TableSharedKeyCredential(azuriteAccountName, azuriteAccountKey));

            var containerClient = tableServiceClient.GetTableClient("Products");

            var products = containerClient.QueryAsync<TableEntity>();

            var response = new ProductCatalogProductGetByIdResponseModel();

            await foreach (var product in products)
            {
                if (product["RowKey"].ToString() != productId!.Value.ToString())
                {
                    continue;
                }

                response = new ProductCatalogProductGetByIdResponseModel()
                {
                    Id = Convert.ToInt64(product["RowKey"]),
                    Name = product["RowKey"].ToString(),
                    Description = product["Description"].ToString(),
                    Price = Convert.ToDouble(product["Price"]),
                    Quantity = Convert.ToInt64(product["Quantity"]),
                    Category = product["Category"].ToString()
                };
            }

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

                ServiceEventSource.Current.ServiceMessage(this.Context, $"Working-{++iterations} :---: ReplicaId-{this.Context.InstanceId}");

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
