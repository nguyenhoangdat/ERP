using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Handlers.IssueSlips
{
    public interface IIssueSlipHandler
    {
        /// <summary>
        /// Handles the incoming IssueSlip.
        /// </summary>
        /// <param name="issueSlip">The instance of IssueSlip</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous IssueSlip processing operation. The task result contains the processed IssueSlip entity written to the database.</returns>
        Task<IssueSlip> HandleAsync(IssueSlip issueSlip, CancellationToken cancellationToken = default);
    }
}
