using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdatePositionCommand : IRequest<Position>
    {
        public UpdatePositionCommand(UpdatePositionCommandModel model)
        {
            this.Model = model;
        }
        public UpdatePositionCommand(long id, string name, double width, double height, double depth, double maxWeight, int rating, int reservedUnits)
            : this(new UpdatePositionCommandModel(id, name, width, height, depth, maxWeight, rating, reservedUnits))
        {
        }

        public UpdatePositionCommandModel Model { get; }

        public class UpdatePositionCommandModel
        {
            public UpdatePositionCommandModel(long id, string name, double width, double height, double depth, double maxWeight, int rating, int reservedUnits)
            {
                this.Id = id;
                this.Name = name;
                this.Width = width;
                this.Height = height;
                this.Depth = depth;
                this.MaxWeight = maxWeight;
                this.Rating = rating;
                this.ReservedUnits = reservedUnits;
            }

            public long Id { get; }
            public string Name { get; }
            public double Width { get; }
            public double Height { get; }
            public double Depth { get; }
            public double MaxWeight { get; }

            public int Rating { get; }
            public int ReservedUnits { get; set; }
        }
    }
}
