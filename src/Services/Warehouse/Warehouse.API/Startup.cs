using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Restmium.ERP.Integration.Catalog;
using Restmium.ERP.Integration.Ordering;
using Restmium.ERP.Integration.Supply;
using Restmium.ERP.Services.Warehouse.API.Models.Application.Mapping;
using Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities.Mapping;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Selectors;
using Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Validators;
using Restmium.ERP.Services.Warehouse.Application.Handlers.Integration;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database.Configuration.Setting;
using Restmium.Messaging;
using Restmium.Messaging.Azure.ServiceBus;
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
                options.UseLazyLoadingProxies().UseSqlServer(this.Configuration.GetConnectionString("DatabaseContext"), opt =>
                {
                    opt.EnableRetryOnFailure();
                    opt.MigrationsAssembly("Warehouse.API");
                });
            });

            services.AddMediatR(typeof(CreateWareCommand).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(AddressMappingProfile).Assembly, typeof(PageMappingProfile).Assembly);

            #region EventBus
            bool isServiceBusEnabled = this.Configuration.GetSection("Azure").GetSection("ServiceBus").GetValue("Enabled", false);

            if (isServiceBusEnabled)
            {
                string serviceBusConnectionString = this.Configuration.GetSection("Azure").GetSection("ServiceBus").GetSection("Topic").GetValue<string>("PrimaryConnectionString");
                string subscriptionClientName = this.Configuration.GetSection("Azure").GetSection("ServiceBus").GetSection("Topic").GetValue<string>("SubscriptionName");

                services.AddAzureMessaging(serviceBusConnectionString, subscriptionClientName);

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
                swagger.CustomSchemaIds(x => x.FullName);
            });

            #region Warehouse.Application.DependencyInjection
            #region IOrderValidator
            if (this.Configuration.GetSection("DependencyInjection").GetSection("IOrderValidator").GetValue("DefaultOrderValidator", false))
            {
                services.AddTransient<IOrderValidator, DefaultOrderValidator>();
            }
            #endregion
            #region IIssueSlipPositionSelector
            if (this.Configuration.GetSection("DependencyInjection").GetSection("IIssueSlipPositionSelector").GetValue("DefaultIssueSlipPositionSelector", false))
            {
                services.AddTransient<IIssueSlipPositionSelector, DefaultIssueSlipPositionSelector>();
            }
            #endregion
            #region IReceiptPositionSelector
            if (this.Configuration.GetSection("DependencyInjection").GetSection("IReceiptPositionSelector").GetValue("DefaultReceiptPositionSelector", false))
            {
                services.AddTransient<IReceiptPositionSelector, DefaultReceiptPositionSelector>();
            }
            #endregion
            #endregion

            #region Infrastucture configuration
            services.AddScoped<IMovementSetting>(sp => {
                int? monthsRetentionPeriod = this.Configuration.GetSection("Infrastucture").GetSection("Movement").GetValue<int?>("MonthsRetentionPeriod", null);
                return new MovementSetting(monthsRetentionPeriod);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                // Applies any pending migration to the context
                // app.ApplicationServices.GetRequiredService<DatabaseContext>().Database.Migrate(); // Use when the port 1433 is blocked
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();

                // Database and all its schema is created
                app.ApplicationServices.GetRequiredService<DatabaseContext>().Database.EnsureCreated();
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

            if (this.Configuration.GetSection("Azure").GetSection("ServiceBus").GetValue("Enabled", false))
            {
                this.ConfigureEventBus(app); //EventBus
            }
        }

        #region EventBus
        private void RegisterEventBus(IServiceCollection services)
        {
            services.AddTransient(sp => {
                DatabaseContext context = sp.GetRequiredService<DatabaseContext>();
                ILogger<OrderCreatedIntegrationEventHandler> logger = sp.GetRequiredService<ILogger<OrderCreatedIntegrationEventHandler>>();
                IMediator mediator = sp.GetRequiredService<IMediator>();
                IIssueSlipPositionSelector issueSlipPositionSelector = sp.GetRequiredService<IIssueSlipPositionSelector>();
                return new OrderCreatedIntegrationEventHandler(context, logger, mediator, issueSlipPositionSelector);
            }); // OrderCreatedIntegrationEventHandler
            services.AddTransient(sp => {
                ILogger<ProductCreatedIntegrationEventHandler> logger = sp.GetRequiredService<ILogger<ProductCreatedIntegrationEventHandler>>();
                IMediator mediator = sp.GetRequiredService<IMediator>();
                return new ProductCreatedIntegrationEventHandler(logger, mediator);
            }); // ProductCreatedIntegrationEventHandler
            services.AddTransient(sp => {
                DatabaseContext context = sp.GetRequiredService<DatabaseContext>();
                ILogger<ProductRemovedIntegrationEventHandler> logger = sp.GetRequiredService<ILogger<ProductRemovedIntegrationEventHandler>>();
                IMediator mediator = sp.GetRequiredService<IMediator>();
                return new ProductRemovedIntegrationEventHandler(context, logger, mediator);
            }); // ProductRemovedIntegrationEventHandler
            services.AddTransient(sp => {
                ILogger<ProductRenamedIntegrationEvent> logger = sp.GetRequiredService<ILogger<ProductRenamedIntegrationEvent>>();
                IMediator mediator = sp.GetRequiredService<IMediator>();
                return new ProductRenamedIntegrationEventHandler(logger, mediator);
            }); // ProductRenamedIntegrationEventHandler
            services.AddTransient(sp => {
                DatabaseContext context = sp.GetRequiredService<DatabaseContext>();
                ILogger<SuppliesOrderedIntegrationEventHandler> logger = sp.GetRequiredService<ILogger<SuppliesOrderedIntegrationEventHandler>>();
                IMediator mediator = sp.GetRequiredService<IMediator>();
                IReceiptPositionSelector positionSelector = sp.GetRequiredService<IReceiptPositionSelector>();
                return new SuppliesOrderedIntegrationEventHandler(context, logger, mediator, positionSelector);
            }); // SuppliesOrderedIntegrationEventHandler
        }
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
            eventBus.Subscribe<ProductCreatedIntegrationEvent, ProductCreatedIntegrationEventHandler>();
            eventBus.Subscribe<ProductRemovedIntegrationEvent, ProductRemovedIntegrationEventHandler>();
            eventBus.Subscribe<ProductRenamedIntegrationEvent, ProductRenamedIntegrationEventHandler>();
            eventBus.Subscribe<SuppliesOrderedIntegrationEvent, SuppliesOrderedIntegrationEventHandler>();
        }
        #endregion
    }
}
