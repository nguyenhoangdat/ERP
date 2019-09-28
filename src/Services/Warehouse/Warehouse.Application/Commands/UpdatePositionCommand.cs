using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdatePositionCommand : IRequest<Position>
    {
        public UpdatePositionCommand(long id, string name, double width, double height, double depth, double maxWeight, int reservedUnits)
        {
            if (id <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            this.Id = id;
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            this.MaxWeight = maxWeight;
            this.ReservedUnits = reservedUnits;
        }

        public long Id { get; }
        public string Name { get; }
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        public double MaxWeight { get; }

        public int ReservedUnits { get; set; }
    }
}
