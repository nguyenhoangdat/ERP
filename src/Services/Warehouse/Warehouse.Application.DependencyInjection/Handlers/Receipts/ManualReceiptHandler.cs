using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Factories.Receipts
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
        public ManualReceiptHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        /// <summary>
        /// The instance of DatabaseContext for accessing database
        /// </summary>
        protected DatabaseContext DatabaseContext { get; }

        /// <summary>
        /// Creates a Receipt without assigning positions to the Items.
        /// </summary>
        /// <param name="receipt">The instance of Receipt</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        public async Task<Receipt> HandleAsync(Receipt receipt, CancellationToken cancellationToken = default)
        {
            Receipt entity = (await this.DatabaseContext.Receipts.AddAsync(receipt, cancellationToken)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
