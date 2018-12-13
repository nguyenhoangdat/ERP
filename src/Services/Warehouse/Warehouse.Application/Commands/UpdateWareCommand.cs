using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateWareCommand : IRequest<Ware>
    {
        public UpdateWareCommand(UpdateWareCommandModel model)
        {
            this.Model = model;
        }

        public UpdateWareCommandModel Model { get; }

        // When modifying, modify the UpdateWareCommandHandler as well
        public class UpdateWareCommandModel
        {
            public int Id { get; }
            public string ProductName { get; }

            public double Width { get; }
            public double Height { get; }
            public double Depth { get; }
            public double Weight { get; }
        }
    }
}
