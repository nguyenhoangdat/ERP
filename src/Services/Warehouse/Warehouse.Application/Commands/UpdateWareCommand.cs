using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateWareCommand : IRequest<Ware>
    {
        public UpdateWareCommand(UpdateWareCommandModel ware)
        {
            this.Ware = ware;
        }

        public UpdateWareCommandModel Ware { get; }

        // When modifying, modify the UpdateWareCommandHandler as well
        public class UpdateWareCommandModel
        {
            public int Id { get; }
            public string ProductName { get; }

            public double Width { get; set; }
            public double Height { get; set; }
            public double Depth { get; set; }
            public double Weight { get; set; }
        }
    }
}
