using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeletePositionCommand : IRequest<Position>
    {
        public DeletePositionCommand(DeletePositionCommandModel model)
        {
            this.Model = model;
        }

        public DeletePositionCommandModel Model { get; }

        public class DeletePositionCommandModel
        {
            public DeletePositionCommandModel(long id)
            {
                this.Id = id;
            }

            public long Id { get; }
        }
    }
}
