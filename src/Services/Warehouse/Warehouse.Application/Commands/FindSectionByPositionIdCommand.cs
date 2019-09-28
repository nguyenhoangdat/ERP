using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindSectionByPositionIdCommand : IRequest<Section>
    {
        public FindSectionByPositionIdCommand(long positionId)
        {
            if (positionId <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(positionId));
            }

            this.PositionId = positionId;
        }

        public long PositionId { get; }
    }
}
