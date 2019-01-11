using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateIssueSlipCommand : IRequest<StockTaking>
    {
        public UpdateIssueSlipCommand(UpdateIssueSlipCommandModel model)
        {
            this.Model = model;
        }

        public UpdateIssueSlipCommandModel Model { get; }

        public class UpdateIssueSlipCommandModel
        {
            public UpdateIssueSlipCommandModel(long id, string name, DateTime utcDispatchDate, DateTime utcDeliveryDate, List<Item> items)
            {
                this.Id = id;
                this.Name = name;
                this.UtcDispatchDate = utcDispatchDate;
                this.UtcDeliveryDate = utcDeliveryDate;
                this.Items = items;
            }

            public long Id { get; }
            public string Name { get; }
            public DateTime UtcDispatchDate { get; }
            public DateTime UtcDeliveryDate { get; }

            public List<Item> Items { get; }

            public class Item
            {
                public Item(long issueSlipId, int wareId, long positionId, int requestedUnits, int issuedUnits)
                {
                    this.IssueSlipId = issueSlipId;
                    this.WareId = wareId;
                    this.PositionId = positionId;
                    this.RequstedUnits = requestedUnits;
                    this.IssuedUnits = issuedUnits;
                }

                public long IssueSlipId { get; }
                public int WareId { get; }
                public long PositionId { get; }

                public int RequstedUnits { get; }
                public int IssuedUnits { get; }
            }
        }
    }
}
