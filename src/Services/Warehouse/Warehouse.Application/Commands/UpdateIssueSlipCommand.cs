using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateIssueSlipCommand : IRequest<IssueSlip>
    {
        public UpdateIssueSlipCommand(long id, DateTime utcDispatchDate, DateTime utcDeliveryDate)
        {
            this.Id = id;
            this.UtcDispatchDate = utcDispatchDate;
            this.UtcDeliveryDate = utcDeliveryDate;
        }

        public long Id { get; }
        public DateTime UtcDispatchDate { get; }
        public DateTime UtcDeliveryDate { get; }
    }
}
