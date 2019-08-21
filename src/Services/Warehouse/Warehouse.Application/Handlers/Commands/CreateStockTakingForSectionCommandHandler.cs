using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class CreateStockTakingForSectionCommandHandler : IRequestHandler<CreateStockTakingForSectionCommand, StockTaking>
    {
        public CreateStockTakingForSectionCommandHandler(DatabaseContext context, IMediator mediator)
        {
            this.DatabaseContext = context;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<StockTaking> Handle(CreateStockTakingForSectionCommand request, CancellationToken cancellationToken)
        {
            // Ensure that Position with specified Id exists
            Section section = this.DatabaseContext.Sections.FirstOrDefault(x => x.Id == request.SectionId);
            if (section == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Section_EntityNotFoundException"], request.SectionId));
            }

            // Create Model.Items for Positions
            IEnumerable<Position> positions = this.DatabaseContext.Positions.Where(x => x.SectionId == request.SectionId).ToList();
            List<CreateStockTakingCommand.Item> items = new List<CreateStockTakingCommand.Item>();
            foreach (Position item in positions)
            {
                items.Add(new CreateStockTakingCommand.Item(item.GetWare().Id, item.Id, item.CountWare(), 0));
            }

            // Create StockTaking through command
            StockTaking stockTaking = await this.Mediator.Send(new CreateStockTakingCommand(string.Format(Resources.Exceptions.Values["StockTaking_Name_Section"], section.Id), items), cancellationToken);

            // Publish DomainEvent that the StockTaking has been created
            await this.Mediator.Publish(new StockTakingCreatedDomainEvent(stockTaking), cancellationToken);

            return stockTaking;
        }
    }
}
