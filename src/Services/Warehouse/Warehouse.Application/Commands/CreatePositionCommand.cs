using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreatePositionCommand : IRequest<Position>
    {
        public CreatePositionCommand(string name, double width, double height, double depth, double maxWeight, int sectionId)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            this.MaxWeight = maxWeight;
            this.SectionId = sectionId;
        }

        public string Name { get; }
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        public double MaxWeight { get; }

        public int SectionId { get; }
    }
}
