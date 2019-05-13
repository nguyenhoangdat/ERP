using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class StoreReceiptItemAtPositionCommandHandler : IRequestHandler<StoreReceiptItemAtPositionCommand, Position>
    {
        public StoreReceiptItemAtPositionCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Position> Handle(StoreReceiptItemAtPositionCommand request, CancellationToken cancellationToken)
        {
            // Find Receipt.Item and throw an exception if not found
            Receipt.Item item = await this.DatabaseContext.ReceiptItems.FindAsync(new object[] { request.ReceiptId, request.WareId }, cancellationToken);
            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["ReceiptItem_EntityNotFoundException"], request.ReceiptId, request.WareId));
            }
            // Find Position and throw an exception if not found
            Position position = await this.DatabaseContext.Positions.FindAsync(new object[] { request.PositionCount.Position.Id }, cancellationToken);
            if (position == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Position_EntityNotFoundException"], request.PositionCount.Position.Id));
            }

            // Create Movement
            await this.Mediator.Send(new CreateMovementCommand(item.WareId, position.Id, Movement.Direction.In, request.PositionCount.Count));

            return position;
        }
    }
}
