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
        public UpdateIssueSlipCommand(long id, DateTime utcDispatchDate, DateTime utcDeliveryDate, DateTime? utcProcessed) : this(id, utcDispatchDate, utcDeliveryDate)
        {
            this.UtcProcessed = utcProcessed;
        }
        public UpdateIssueSlipCommand(long id, DateTime utcDispatchDate, DateTime utcDeliveryDate, DateTime? utcProcessed, DateTime? utcDispatched) : this(id, utcDispatchDate, utcDeliveryDate, utcProcessed)
        {
            this.UtcDispatched = utcDispatched;
        }

        public long Id { get; }
        public DateTime UtcDispatchDate { get; }
        public DateTime UtcDeliveryDate { get; }
        public DateTime? UtcProcessed { get; }
        public DateTime? UtcDispatched { get; }
    }
}
