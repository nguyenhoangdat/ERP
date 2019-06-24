using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Factories.Receipts
{
    /// <summary>
    /// Interface for handlers handling incoming wares.
    /// </summary>
    public interface IReceiptHandler
    {
        /// <summary>
        /// Handles the incoming Receipt.
        /// </summary>
        /// <param name="receipt">The instance of Receipt</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous Receipt processing operation. The task result contains the processed Receipt entity written to the database.</returns>
        Task<Receipt> HandleAsync(Receipt receipt, CancellationToken cancellationToken = default);
    }
}
