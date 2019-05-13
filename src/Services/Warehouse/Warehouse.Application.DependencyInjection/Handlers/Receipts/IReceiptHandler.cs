using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Handlers.Receipts
{
    /// <summary>
    /// Interface for handlers handling incoming wares.
    /// </summary>
    public interface IReceiptHandler
    {
        /// <summary>
        /// Handles the incoming ReceiptDTO.
        /// </summary>
        /// <param name="receipt">The instance of ReceiptDTO</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        Task<Receipt> HandleAsync(ReceiptDTO receiptDTO, CancellationToken cancellationToken = default(CancellationToken));

        //TODO: IMPORTANT - Change to RECEIPT (DO NOT USE DTO)
    }
}
