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
        public FindIssueSlipToProcessInSectionCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IssueSlip> Handle(FindIssueSlipToProcessInSectionCommand request, CancellationToken cancellationToken)
        {
            // Ensure that the Section exists and throw an EntityNotFoundException if it doesn't
            Section section = this.DatabaseContext.Sections.FirstOrDefault(x => x.Id == request.SectionId);
            if (section == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Section_EntityNotFoundException"], request.SectionId));
            }

            // Return IssueSlip that needs to be processed ASAP
            return this.DatabaseContext.IssueSlips
                .Where(x => x.HasSectionIdWithUnissuedUnits(section.Id) && x.UtcProcessed == null && x.UtcMovedToBin == null)
                .OrderBy(x => x.UtcDeliveryDate)
                .FirstOrDefault();
        }
    }
}
