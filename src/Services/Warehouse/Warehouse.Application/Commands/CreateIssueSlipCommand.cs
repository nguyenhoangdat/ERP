using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateIssueSlipCommand : IRequest<IssueSlip>
    {
        public CreateIssueSlipCommand(long orderId, DateTime utcDispatchDate, DateTime utcDeliveryDate, IEnumerable<Item> items)
        {
            this.OrderId = orderId;
            this.UtcDispatchDate = utcDispatchDate;
            this.UtcDeliveryDate = utcDeliveryDate;
            this.Items = items;
        }

        public long OrderId { get; }
        public DateTime UtcDispatchDate { get; }
        public DateTime UtcDeliveryDate { get; }

        public IEnumerable<Item> Items { get; }

        public class Item
        {
            public Item(Ware ware, long? positionId, int requestedUnits)
            {
                this.Ware = ware;
                this.PositionId = positionId;
                this.RequstedUnits = requestedUnits;
            }

            public Ware Ware { get; }
            public long? PositionId { get; }
            public int RequstedUnits { get; }
        }
    }
}
