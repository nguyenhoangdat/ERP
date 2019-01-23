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

        public CreateIssueSlipCommandModel Model { get; }

        public class CreateIssueSlipCommandModel
        {
            public CreateIssueSlipCommandModel(string name, long orderId, DateTime utcDispatchDate, DateTime utcDeliveryDate, List<Item> items)
            {
                this.Name = name;
                this.OrderId = orderId;
                this.UtcDispatchDate = utcDispatchDate;
                this.UtcDeliveryDate = utcDeliveryDate;
                this.Items = items;
            }

            public string Name { get; }
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
