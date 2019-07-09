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
        public FindIssueSlipItemToProcessInSectionByIssueSlipIdCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IssueSlip.Item> Handle(FindIssueSlipItemToProcessInSectionByIssueSlipIdCommand request, CancellationToken cancellationToken)
        {
            if (await this.DatabaseContext.Sections.FindAsync(new object[] { request.SectionId }, cancellationToken) == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Section_EntityNotFoundException"], request.SectionId));
            }
            if (await this.DatabaseContext.IssueSlips.FindAsync(new object[] { request.IssueSlipId }, cancellationToken) == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["IssueSlip_EntityNotFoundException"], request.IssueSlipId));
            }

            return this.DatabaseContext.IssueSlipItems
                .FirstOrDefault(x =>
                    x.IssueSlipId == request.IssueSlipId &&
                    x.Position.SectionId == request.SectionId &&
                    x.IssuedUnits < x.RequestedUnits &&
                    this.Mediator.Send(new GetWareAvailabilityInSectionCommand(x.WareId, x.Position.SectionId), cancellationToken).Result.UnitsAvailable > 0); 
        }
    }
}
