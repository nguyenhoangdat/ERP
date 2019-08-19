using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindPositionsForReceiptItemCommandHandler : IRequestHandler<FindPositionsForReceiptItemCommand, IEnumerable<PositionCount>>
    {
        public FindPositionsForReceiptItemCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<IEnumerable<PositionCount>> Handle(FindPositionsForReceiptItemCommand request, CancellationToken cancellationToken)
        {
            Receipt.Item item = await this.DatabaseContext.ReceiptItems.FindAsync(new object[] { request.ReceiptId, request.WareId }, cancellationToken);
            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["ReceiptItem_EntityNotFoundException"], request.ReceiptId, request.WareId));
            }

            IEnumerable<Position> positions = this.DatabaseContext.Positions
                .Where(x => x.GetWare() == item.Ware)
                .OrderByDescending(x => x.MaxLoadCapacity(item.Ware))
                .ToList();

            int itemsToPlace = item.CountReceived;
            List<PositionCount> positionCounts = new List<PositionCount>();

            foreach (Position position in positions)
            {
                int possibleToPlace = position.MaxLoadCapacity(item.Ware) - position.CountWare();

                if (possibleToPlace > 0)
                {
                    possibleToPlace = (itemsToPlace < possibleToPlace) ? itemsToPlace : possibleToPlace;

                    positionCounts.Add(new PositionCount(position, possibleToPlace));
                    itemsToPlace -= possibleToPlace;

                    await this.Mediator.Send(new AssignReceiptItemToPositionCommand(item.ReceiptId, position.Id, item.WareId), cancellationToken);

                    if (itemsToPlace == 0)
                    {
                        break;
                    }
                }
            }

            if (itemsToPlace > 0)
            {
                throw new WarehouseFullException(string.Format(Resources.Exceptions.Values["ReceiptItem_Create_WarehouseFullException"], request.ReceiptId, request.WareId));
            }

            return positionCounts;
        }
    }
}
