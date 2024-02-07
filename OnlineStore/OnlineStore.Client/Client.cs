using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using OnlineStore.Client.Areas.Identity.Data;
using OnlineStore.Client.Data;
using System.Fabric;

namespace OnlineStore.Client
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance.
    /// </summary>
    internal sealed class Client : StatelessService
    {
        public Client(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        var builder = WebApplication.CreateBuilder();

                        builder.Services.AddSingleton<StatelessServiceContext>(serviceContext);
                        builder.WebHost
                                    .UseKestrel()
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                    .UseUrls(url);
                        
                        // Add services to the container.

                        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=OnlineStore.Client.db"));
                        builder.Services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
                        builder.Services.AddControllersWithViews();
                        builder.Services.AddRazorPages();
                        builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
                        builder.Services.AddHttpClient();

                        var app = builder.Build();
                        
                        // Configure the HTTP request pipeline.
                        if (app.Environment.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }
                        else
                        {
                            app.UseExceptionHandler("/Home/Error");
                            app.UseHsts();
                        }

                        app.UseStaticFiles();
                        app.UseRouting();
                        app.UseMvcWithDefaultRoute();
                        app.UseAuthorization();

                        app.MapControllers();
                        app.Run();

                        return app;
                    }))
            };
        }
    }
}
