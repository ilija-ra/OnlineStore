using Azure;
using Azure.Data.Tables;
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
        string azuriteAccountName = "devstoreaccount1";
        string azuriteAccountKey = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";
        Uri azuriteTableEndpoint = new Uri("http://127.0.0.1:10002/devstoreaccount1");

        public Order(StatelessServiceContext context)
            : base(context)
        { }

        #region IOrderImplementation

        public async Task<OrderPurchaseConfirmResponseModel> PurchaseConfirm(OrderPurchaseConfirmRequestModel model)
        {
            model.PurchaseDate = DateTime.Now;

            // Create a service client
            TableServiceClient serviceClient = new TableServiceClient(azuriteTableEndpoint, new TableSharedKeyCredential(azuriteAccountName, azuriteAccountKey));

            // Create a table client for "Products" table
            TableClient tableClient = serviceClient.GetTableClient("PurchaseHistory");

            var entity = new TableEntity(Context.PartitionId.ToString(), Guid.NewGuid().ToString())
            {
                { "PurchasedProducts", model.Products! },
                { "PurchaseDate", model.PurchaseDate },
                { "CardNumber", model.CardNumber },
                { "TotalAmount", model.TotalAmount },
                { "PaymentMethod", model.PaymentMethod },
                { "UserId", model.UserId },
            };

            try
            {
                await tableClient.AddEntityAsync(entity);
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine($"Failed to add entity: {ex.Message}");
            }

            return new OrderPurchaseConfirmResponseModel();
        }

        public async Task<OrderPurchaseGetAllResponseModel> PurchaseGetAll(string? userId)
        {
            var response = new OrderPurchaseGetAllResponseModel();

            var tableServiceClient = new TableServiceClient(azuriteTableEndpoint, new TableSharedKeyCredential(azuriteAccountName, azuriteAccountKey));

            var containerClient = tableServiceClient.GetTableClient("PurchaseHistory");

            var purchaseHistory = containerClient.QueryAsync<TableEntity>();

            await foreach (var purchase in purchaseHistory)
            {
                response.Items!.Add(new OrderPurchaseGetAllItemModel()
                {
                    Id = Convert.ToInt64(purchase["RowKey"]),
                    PurchasedProducts = purchase["PurchasedProducts"].ToString(),
                    PurchaseDate = Convert.ToDateTime(purchase["PurchaseDate"]),
                    CardNumber = purchase["CardNumber"].ToString(),
                    TotalAmount = Convert.ToDouble(purchase["TotalAmount"]),
                    PaymentMethod = purchase["PaymentMethod"].ToString(),
                    UserId = purchase["UserId"].ToString()
                });
            }

            //response.Items = new List<OrderPurchaseGetAllItemModel>()
            //{
            //    new OrderPurchaseGetAllItemModel(){ Id = 1, PurchaseDate = DateTime.Now, PurchasedProducts = new List<string>(){ "Banana", "Apple", "Milk" }, TotalAmount = 1500, PaymentMethod = "PayPal", UserId = "111" },
            //    new OrderPurchaseGetAllItemModel(){ Id = 2, PurchaseDate = DateTime.Now, PurchasedProducts = new List<string>(){ "Cheese", "Snickers", "Twix" }, TotalAmount = 1200, PaymentMethod = "Cash On Delivery", UserId = "111" },
            //    new OrderPurchaseGetAllItemModel(){ Id = 3, PurchaseDate = DateTime.Now, PurchasedProducts = new List<string>(){ "Strawberry", "Berry" }, TotalAmount = 1700, PaymentMethod = "Cash On Delivery", UserId = "111" }
            //};

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
