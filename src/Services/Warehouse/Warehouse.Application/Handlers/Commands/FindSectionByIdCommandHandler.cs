using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
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
            Section section = await this.DatabaseContext.Sections.FindAsync(new object[] { request.SectionId }, cancellationToken);

            if (section == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Section_EntityNotFoundException"], request.SectionId));
            }

            return section;
        }
    }
}
