using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class GetWareAvailabilityInSectionCommand : IRequest<WareAvailabilityInSection>
    {
        public GetWareAvailabilityInSectionCommand(int wareId, int sectionId)
        {
            this.WareId = wareId;
            this.SectionId = sectionId;
        }

        public int WareId { get; }
        public int SectionId { get; }
    }
}
