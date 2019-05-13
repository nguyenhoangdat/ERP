using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateWareCommand : IRequest<Ware>
    {
        public UpdateWareCommand(int wareId, string productName, double width, double height, double depth, double weight)
        {
            this.WareId = wareId;
            this.ProductName = productName;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            this.Weight = weight;
        }

        public int WareId { get; }
        public string ProductName { get; }

        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        public double Weight { get; }
    }
}
