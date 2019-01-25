using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class RelocatePositionCommand : IRequest<Position>
    {
        public RelocatePositionCommand(RelocatePositionCommandModel model)
        {
            this.Model = model;
        }
        public RelocatePositionCommand(long fromPositionWithId, long toPositionWithId)
            : this(new RelocatePositionCommandModel(fromPositionWithId, toPositionWithId))
        {
        }

        public RelocatePositionCommandModel Model { get; }

        public class RelocatePositionCommandModel
        {
            public RelocatePositionCommandModel(long fromPositionWithId, long toPositionWithId)
            {
                this.FromPositionWithId = fromPositionWithId;
                this.ToPositionWithId = toPositionWithId;
            }

            public long FromPositionWithId { get; }
            public long ToPositionWithId { get; }
        }
    }
}
