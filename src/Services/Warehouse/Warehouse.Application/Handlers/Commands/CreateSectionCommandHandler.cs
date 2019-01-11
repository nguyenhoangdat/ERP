using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Section>
    {
        public CreateSectionCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Section> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            Section section = this.DatabaseContext.Sections.Add(new Section(request.Model.Name, request.Model.WarehouseId)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);
            return section;
        }
    }
}
