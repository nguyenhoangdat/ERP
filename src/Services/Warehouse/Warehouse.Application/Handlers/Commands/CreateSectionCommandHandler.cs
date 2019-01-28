using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Section>
    {
        public CreateSectionCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Section> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            Section section = this.DatabaseContext.Sections.Add(new Section(request.Model.Name, request.Model.WarehouseId)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new SectionCreatedDomainEvent(section), cancellationToken);

            return section;
        }
    }
}
