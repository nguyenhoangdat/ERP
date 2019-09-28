using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RestoreSectionFromBinCommand : IRequest<Section>
    {
        public RestoreSectionFromBinCommand(int sectionId)
        {
            if (sectionId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(sectionId));
            }
            this.SectionId = sectionId;
        }

        public int SectionId { get; }
    }
}
