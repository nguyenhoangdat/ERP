using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class MoveSectionToBinCommand : IRequest<Section>
    {
        public MoveSectionToBinCommand(int sectionId, bool movedToBinInCascade)
        {
            if (sectionId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(sectionId));
            }

            this.SectionId = sectionId;
            this.MovedToBinInCascade = movedToBinInCascade;
        }

        public int SectionId { get; }
        public bool MovedToBinInCascade { get; }
    }
}
