using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateIssueSlipCommand : IRequest<IssueSlip>
    {
        public UpdateIssueSlipCommand(UpdateIssueSlipCommandModel model)
        {
            this.Model = model;
        }
        public UpdateIssueSlipCommand(long id, long orderId, string name, DateTime utcDispatchDate, DateTime utcDeliveryDate, List<UpdateIssueSlipCommand.UpdateIssueSlipCommandModel.Item> items)
            : this(new UpdateIssueSlipCommandModel(id, orderId, name, utcDispatchDate, utcDeliveryDate, items)) { }

        public UpdateIssueSlipCommandModel Model { get; }

        public class UpdateIssueSlipCommandModel
        {
            public UpdateIssueSlipCommandModel(long id, long orderId, string name, DateTime utcDispatchDate, DateTime utcDeliveryDate, List<Item> items)
            {
                this.Id = id;
                this.Name = name;
                this.OrderId = orderId;
                this.UtcDispatchDate = utcDispatchDate;
                this.UtcDeliveryDate = utcDeliveryDate;
                this.Items = items;
            }

            public long Id { get; }
            public string Name { get; }
            public long OrderId { get; }
            public DateTime UtcDispatchDate { get; }
            public DateTime UtcDeliveryDate { get; }

            public List<Item> Items { get; }

            public class Item
            {
                public Item(long issueSlipId, int wareId, long positionId, int requestedUnits, int issuedUnits, int employeeId)
                {
                    this.IssueSlipId = issueSlipId;
                    this.WareId = wareId;
                    this.PositionId = positionId;
                    this.RequstedUnits = requestedUnits;
                    this.IssuedUnits = issuedUnits;
                    this.EmployeeId = employeeId;
                }
                public Item(long issueSlipId, int wareId, long positionId, int requestedUnits, int issuedUnits) : this(issueSlipId, wareId, positionId, requestedUnits, issuedUnits, 0)
                {

                }

                public long IssueSlipId { get; }
                public int WareId { get; }
                public long PositionId { get; }

                public int RequstedUnits { get; }
                public int IssuedUnits { get; }
                public int EmployeeId { get; }
            }
        }
    }
}
