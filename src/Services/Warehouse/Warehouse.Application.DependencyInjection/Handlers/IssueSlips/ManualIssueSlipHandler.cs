using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Handlers.IssueSlips
{
    public class ManualIssueSlipHandler : IIssueSlipHandler
    {
        public ManualIssueSlipHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        /// <summary>
        /// Processes IssueSlip and saves it into the database
        /// </summary>
        /// <param name="issueSlip"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IssueSlip> HandleAsync(IssueSlip issueSlip, CancellationToken cancellationToken = default)
        {
            /*
             * IssueSlip will be processed later on manually
             */

            IssueSlip entity = (await this.DatabaseContext.IssueSlips.AddAsync(issueSlip, cancellationToken)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
