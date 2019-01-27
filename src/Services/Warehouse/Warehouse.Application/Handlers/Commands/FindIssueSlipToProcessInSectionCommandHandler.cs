using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindIssueSlipToProcessInSectionCommandHandler : IRequestHandler<FindIssueSlipToProcessInSectionCommand, IssueSlip>
    {
        protected const string FindIssueSlipToProcessInSectionCommandHandler_EntityNotFoundException = "Section (Id={0}) not found!";

        public FindIssueSlipToProcessInSectionCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        public DatabaseContext DatabaseContext { get; }

        public async Task<IssueSlip> Handle(FindIssueSlipToProcessInSectionCommand request, CancellationToken cancellationToken)
        {
            // Ensure that the Section exists and if not then throw an EntityNotFoundException
            Section section = await this.DatabaseContext.Sections.FindAsync(new object[] { request.Model.SectionId }, cancellationToken);
            if (section == null)
            {
                throw new EntityNotFoundException(string.Format(FindIssueSlipToProcessInSectionCommandHandler_EntityNotFoundException, request.Model.SectionId));
            }

            // Return IssueSlip that need to be processed ASAP
            return this.DatabaseContext.IssueSlips
                .Where(x => x.GetSection().Id == request.Model.SectionId)
                .OrderBy(x => x.UtcDeliveryDate)
                .FirstOrDefault();
        }
    }
}
