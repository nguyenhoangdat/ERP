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
    public class FindSectionByIdCommandHandler : IRequestHandler<FindSectionByIdCommand, Section>
    {
        public FindSectionByIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Section> Handle(FindSectionByIdCommand request, CancellationToken cancellationToken)
        {
            Section section = this.DatabaseContext.Sections.FirstOrDefault(x => x.Id == request.SectionId);

            if (section == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Section_EntityNotFoundException, request.SectionId));
            }

            return section;
        }
    }
}
