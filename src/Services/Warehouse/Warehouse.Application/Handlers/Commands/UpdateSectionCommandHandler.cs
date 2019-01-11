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
    public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Section>
    {
        protected const string UpdateSectionCommandHandlerEntityNotFoundException = "Unable to update Section with id={0}. Section not found!";

        public UpdateSectionCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Section> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Sections.Any(x => x.Id == request.Model.Id))
            {
                throw new EntityNotFoundException(string.Format(UpdateSectionCommandHandlerEntityNotFoundException, request.Model.Id));
            }

            Section section = this.DatabaseContext.Sections.Find(request.Model.Id);
            section.Name = request.Model.Name;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);
            return section;
        }
    }
}
