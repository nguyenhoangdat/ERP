using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindIssueSlipItemToProcessInSectionByIssueSlipIdCommandHandler : IRequestHandler<FindIssueSlipItemToProcessInSectionByIssueSlipIdCommand, IssueSlip.Item>
    {
        public FindIssueSlipItemToProcessInSectionByIssueSlipIdCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IssueSlip.Item> Handle(FindIssueSlipItemToProcessInSectionByIssueSlipIdCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Sections.Any(x => x.Id == request.SectionId))
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Section_EntityNotFoundException, request.SectionId));
            }
            if (!this.DatabaseContext.IssueSlips.Any(x => x.Id == request.IssueSlipId))
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.IssueSlip_EntityNotFoundException, request.IssueSlipId));
            }

            return this.DatabaseContext.IssueSlipItems
                .FirstOrDefault(x =>
                    x.IssueSlipId == request.IssueSlipId &&
                    x.Position.SectionId == request.SectionId &&
                    x.IssuedUnits < x.RequestedUnits &&
                    x.Position.Movements.OrderByDescending(e => e.UtcCreated).First().CountTotal > 0);
        }
    }
}
