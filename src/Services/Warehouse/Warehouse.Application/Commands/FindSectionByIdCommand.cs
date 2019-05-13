using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindSectionByIdCommand : IRequest<Section>
    {
        public FindSectionByIdCommand(int sectionId)
        {
            this.SectionId = sectionId;
        }

        public int SectionId { get; }
    }
}
