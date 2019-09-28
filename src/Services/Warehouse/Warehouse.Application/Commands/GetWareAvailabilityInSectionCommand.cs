using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Models;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class GetWareAvailabilityInSectionCommand : IRequest<WareAvailabilityInSection>
    {
        public GetWareAvailabilityInSectionCommand(int wareId, int sectionId)
        {
            this.WareId = wareId;

            if (sectionId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(sectionId));
            }
            this.SectionId = sectionId;
        }

        public int WareId { get; }
        public int SectionId { get; }
    }
}
