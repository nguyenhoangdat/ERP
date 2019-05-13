using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, Section>
    {
        public DeleteSectionCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Section> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Sections.Any(x => x.Id == request.Id))
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Section_Delete_EntityNotFoundException"], request.Id));
            }

            Section section = this.DatabaseContext.Sections.Remove(this.DatabaseContext.Sections.Find(request.Id)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new SectionDeletedDomainEvent(section), cancellationToken);

            return section;
        }
    }
}
