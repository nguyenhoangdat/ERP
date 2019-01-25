using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreatePositionCommand : IRequest<Position>
    {
        public CreatePositionCommand(CreatePositionCommandModel model)
        {
            this.Model = model;
        }
        public CreatePositionCommand(string name, double width, double height, double depth, double maxWeight, int sectionId, int rating)
            : this(new CreatePositionCommandModel(name, width, height, depth, maxWeight, sectionId, rating))
        {
        }

        public CreatePositionCommandModel Model { get; }

        public class CreatePositionCommandModel
        {
            public CreatePositionCommandModel(string name, double width, double height, double depth, double maxWeight, int sectionId, int rating)
            {
                this.Name = name;
                this.Width = width;
                this.Height = height;
                this.Depth = depth;
                this.MaxWeight = maxWeight;
                this.SectionId = sectionId;
                this.Rating = rating;
            }
            public CreatePositionCommandModel(string name, double width, double height, double depth, double maxWeight, Section section, int rating) : this(name, width, height, depth, maxWeight, section.Id, rating)
            {
            }

            public string Name { get; }
            public double Width { get; }
            public double Height { get; }
            public double Depth { get; }
            public double MaxWeight { get; }

            public int SectionId { get; }
            public int Rating { get; }
        }
    }
}
