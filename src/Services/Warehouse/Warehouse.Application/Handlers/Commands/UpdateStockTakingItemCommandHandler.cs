using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateStockTakingItemCommandHandler : IRequestHandler<UpdateStockTakingItemCommand, StockTaking.Item>
    {
        public UpdateStockTakingItemCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<StockTaking.Item> Handle(UpdateStockTakingItemCommand request, CancellationToken cancellationToken)
        {
            // Find StockTaking.Item and throw an exception if not found
            StockTaking.Item item = await this.DatabaseContext.StockTakingItems.FindAsync(new object[]
            {
                request.Model.StockTakingId,
                request.Model.PositionId
            }, cancellationToken);
            if (item == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["StockTakingItem_EntityNotFoundException"], request.Model.StockTakingId, request.Model.PositionId));
            }

            // Update and Save
            item = this.DatabaseContext.StockTakingItems.Update(new StockTaking.Item(request.Model.StockTakingId, request.Model.WareId, request.Model.PositionId, request.Model.CurrentStock, request.Model.CountedStock, request.Model.EmployeeId, request.Model.UtcCounted)).Entity;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            // Publish Domain Event that the StockTaking.Item has been updated
            await this.Mediator.Publish(new StockTakingItemUpdatedDomainEvent(item));

            return item;
        }
    }
}
