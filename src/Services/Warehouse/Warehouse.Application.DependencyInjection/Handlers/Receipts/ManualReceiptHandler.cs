using Microsoft.Extensions.Logging;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Handlers.Receipts
{
    /// <summary>
    /// Creates a Receipt without assigning positions to the Items
    /// </summary>
    public class ManualReceiptHandler : IReceiptHandler
    {
        /// <summary>
        /// Creates a Manual Receipt Handler.
        /// </summary>
        /// <param name="context">DatabaseContext for accessing database</param>
        /// <param name="logger">Logger instance for ManualReceiptHandler</param>
        public ManualReceiptHandler(DatabaseContext context, ILogger<ManualReceiptHandler> logger)
        {
            this.DatabaseContext = context;
            this.Logger = logger;
        }

        /// <summary>
        /// The instance of DatabaseContext for accessing database
        /// </summary>
        protected DatabaseContext DatabaseContext { get; }
        /// <summary>
        /// The instance of Logger for logging
        /// </summary>
        protected ILogger<ManualReceiptHandler> Logger { get; }

        /// <summary>
        /// Creates a Receipt without assigning positions to the Items.
        /// </summary>
        /// <param name="receipt">The instance of ReceiptDTO</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        public async Task<Receipt> HandleAsync(ReceiptDTO receiptDTO, CancellationToken cancellationToken = default(CancellationToken))
        {
            HashSet<Receipt.Item> items = new HashSet<Receipt.Item>(receiptDTO.Items.Count);

            // Create list of Receipt.Item from ReceiptDTO.Item
            foreach (ReceiptDTO.Item item in receiptDTO.Items)
            {
                Ware ware = this.DatabaseContext.Wares.Where(x => x.ProductId == item.ProductId).FirstOrDefault();

                if (ware == null)
                {
                    this.Logger.LogCritical(Resources.Exceptions.Values["Ware_ProductId_EntityNotFoundException"], item.ProductId);
                }
                else
                {
                    items.Add(new Receipt.Item(ware.Id, item.CountOrdered));
                }
            }

            // Insert receipt into the database
            Receipt receipt = this.DatabaseContext.Receipts.AddAsync(new Receipt(receiptDTO.UtcExpected, items)).Result.Entity;

            // Commit transaction
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return receipt;
        }
    }
}
