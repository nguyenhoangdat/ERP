using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Restmium.ERP.BuildingBlocks.EventBus;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.BuildingBlocks.EventBusServiceBus;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Handlers.Integration;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Integration.Events;
using System.Reflection;

namespace Restmium.ERP.Services.Warehouse.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddHealthChecks()
                .AddDbContextCheck<DatabaseContext>();

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(this.Configuration.GetConnectionString("DefaultMSSQLConnection"), opt => opt.EnableRetryOnFailure());
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddMediatR(typeof(CreateWareCommand).GetTypeInfo().Assembly);

            #region EventBus
            bool isServiceBusEnabled = this.Configuration.GetValue("AzureServiceBusEnabled", false);

            if (isServiceBusEnabled)
            {
                services.AddSingleton<IServiceBusPersistentConnection>(sp =>
                {
                    ILogger<DefaultServiceBusPersisterConnection> logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();

                    string serviceBusConnectionString = this.Configuration.GetConnectionString("AzureServiceBusTopicConnection");
                    ServiceBusConnectionStringBuilder serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);

                    return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
                });
                this.RegisterEventBus(services);
            }
            #endregion

            services.AddSwaggerGen(swagger => {
                swagger.SwaggerDoc(
                    "2019.1.1",
                    new Swashbuckle.AspNetCore.Swagger.Info()
                    {
                        Title = "Warehouse.API",
                        Version = "2019.1.1"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseHealthChecks("/health");

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/2019.1.1/swagger.json", "Warehouse.API 2019.1.1");
                c.RoutePrefix = string.Empty; //Make Swagger as default Index
                c.DocumentTitle = "Warehouse.API Documentation";
            });

            if (this.Configuration.GetValue("AzureServiceBusEnabled", false))
            {
                this.ConfigureEventBus(app); //EventBus
            }
        }

        #region EventBus
        private void RegisterEventBus(IServiceCollection services)
        {
            string subscriptionClientName = this.Configuration["AzureSubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
            {
                IServiceBusPersistentConnection persistentConnection = sp.GetRequiredService<IServiceBusPersistentConnection>();
                ILogger<EventBusServiceBus> logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                IEventBusSubscriptionsManager subscriptionManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBusServiceBus(sp, persistentConnection, logger, subscriptionManager, subscriptionClientName);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            //TODO: Check
            // Do I need Loggers? - Ex. when Entity is not found
            services.AddTransient<OrderCreatedIntegrationEventHandler>();
            services.AddTransient<ProductAddedIntegrationEventHandler>();
            services.AddTransient<ProductRemovedIntegrationEventHandler>();
            services.AddTransient<ProductRenamedIntegrationEventHandler>();
            services.AddTransient<SuppliesOrderedIntegrationEventHandler>();
        }
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            //TODO: Add missing subscriptions
            eventBus.Subscribe<ProductAddedIntegrationEvent, ProductAddedIntegrationEventHandler>();
            eventBus.Subscribe<ProductRemovedIntegrationEvent, ProductRemovedIntegrationEventHandler>();
            eventBus.Subscribe<SuppliesOrderedIntegrationEvent, SuppliesOrderedIntegrationEventHandler>();
        }
        #endregion
    }
}
