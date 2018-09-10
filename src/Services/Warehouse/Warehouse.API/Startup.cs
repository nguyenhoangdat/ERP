using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Restmium.ERP.BuildingBlocks.EventBus;
using Restmium.ERP.BuildingBlocks.EventBus.Abstractions;
using Restmium.ERP.BuildingBlocks.EventBusServiceBus;
using Warehouse.API.Integration.Events;
using Warehouse.API.Integration.Handlers;
using Warehouse.API.Models;

namespace Warehouse.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<DatabaseContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultMSSQLConnection"), opt => opt.EnableRetryOnFailure()));

            #region EventBus
            bool isServiceBusEnabled = Configuration.GetValue("ServiceBusEnabled", false);

            if (isServiceBusEnabled)
            {
                services.AddSingleton<IServiceBusPersistentConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();

                    var serviceBusConnectionString = Configuration.GetConnectionString("AzureServiceBusTopicConnection");
                    var serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);

                    return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
                });
                RegisterEventBus(services);
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
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/2019.1.1/swagger.json", "Warehouse.API 2019.1.1");
            });

            bool isServiceBusEnabled = Configuration.GetValue("ServiceBusEnabled", false);

            if (isServiceBusEnabled)
            {
                ConfigureEventBus(app); //EventBus
            }            
        }

        #region EventBus
        private void RegisterEventBus(IServiceCollection services)
        {
            var subscriptionClientName = Configuration["SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
            {
                var persistentConnection = sp.GetRequiredService<IServiceBusPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                var subscriptionManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBusServiceBus(sp, persistentConnection, logger, subscriptionManager, subscriptionClientName);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            services.AddTransient(sp => {
                var context = sp.GetRequiredService<DatabaseContext>();
                var logger = sp.GetRequiredService<ILogger<ProductAddedIntegrationEventHandler>>();
                return new ProductAddedIntegrationEventHandler(context, logger);
            }); // ProductAddedIntegrationEventHandler
            services.AddTransient(sp => {
                var context = sp.GetRequiredService<DatabaseContext>();
                var logger = sp.GetRequiredService<ILogger<ProductRemovedIntegrationEventHandler>>();
                return new ProductRemovedIntegrationEventHandler(context, logger);
            }); // ProductRemovedIntegrationEventHandler
            services.AddTransient(sp => {
                var context = sp.GetRequiredService<DatabaseContext>();
                var logger = sp.GetRequiredService<ILogger<ProductRenamedIntegrationEventHandler>>();
                return new ProductRenamedIntegrationEventHandler(context, logger);
            }); // ProductRenamedIntegrationEventHandler
        }
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<ProductAddedIntegrationEvent, ProductAddedIntegrationEventHandler>();
            eventBus.Subscribe<ProductRemovedIntegrationEvent, ProductRemovedIntegrationEventHandler>();
        }
        #endregion
    }
}
