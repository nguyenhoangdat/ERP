using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class CreateIssueSlipCommand : IRequest<IssueSlip>
    {
        public CreateIssueSlipCommand(CreateIssueSlipCommandModel model)
        {
            this.Model = model;
        }
        public CreateIssueSlipCommand(long orderId, DateTime utcDispatchDate, DateTime utcDeliveryDate, List<CreateIssueSlipCommandModel.Item> items)
            : this(new CreateIssueSlipCommandModel(orderId, utcDispatchDate, utcDeliveryDate, items)) { }

        public CreateIssueSlipCommandModel Model { get; }

        public class CreateIssueSlipCommandModel
        {
            public CreateIssueSlipCommandModel(long orderId, DateTime utcDispatchDate, DateTime utcDeliveryDate, List<Item> items)
            {
                this.OrderId = orderId;
                this.UtcDispatchDate = utcDispatchDate;
                this.UtcDeliveryDate = utcDeliveryDate;
                this.Items = items;
            }

            public long OrderId { get; }
            public DateTime UtcDispatchDate { get; }
            public DateTime UtcDeliveryDate { get; }

            public List<Item> Items { get; }

            public class Item
            {
                public Item(Ware ware, int requestedUnits)
                {
                    this.Ware = ware;
                    this.RequstedUnits = requestedUnits;
                }

                public Ware Ware { get; }
                public int RequstedUnits { get; }
            }
        }
    }
}
