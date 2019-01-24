using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateIssueSlipItemCommandHandler : IRequestHandler<UpdateIssueSlipItemCommand, IssueSlip.Item>
    {
        public UpdateIssueSlipItemCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        public DatabaseContext DatabaseContext { get; }
        public IMediator Mediator { get; }

        public async Task<IssueSlip.Item> Handle(UpdateIssueSlipItemCommand request, CancellationToken cancellationToken)
        {
            // Find IssueSlip in the Database
            IssueSlip.Item item = this.DatabaseContext.IssueSlipItems.Where(x => x.IssueSlipId == request.Model.IssueSlipId && x.WareId == request.Model.WareId).FirstOrDefault();

            // Update and Save
            item.PositionId = request.Model.PositionId;
            item = this.DatabaseContext.IssueSlipItems.Update(item).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            // Publish DomainEvent
            await this.Mediator.Publish(new IssueSlipItemUpdatedDomainEvent(item));

            return item;
        }
    }
}
