using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteMovementCommand : IRequest<Movement>
    {
        public DeleteMovementCommand(DeleteMovementCommandModel model)
        {
            this.Model = model;
        }

        public DeleteMovementCommandModel Model { get; }

        public class DeleteMovementCommandModel
        {
            public DeleteMovementCommandModel(long id)
            {
                this.Id = id;
            }

            public long Id { get; }
        }
    }
}
