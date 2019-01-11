using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, Section>
    {
        protected const string DeleteSectionCommandHandlerEntityNotFoundException = "Unable to delete Section with id={0}. Section not found!";

        public DeleteSectionCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Section> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Sections.Any(x => x.Id == request.Model.Id))
            {
                throw new EntityNotFoundException(string.Format(DeleteSectionCommandHandlerEntityNotFoundException, request.Model.Id));
            }

            Section section = this.DatabaseContext.Sections.Remove(this.DatabaseContext.Sections.Find(request.Model.Id)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);
            return section;
        }
    }
}
