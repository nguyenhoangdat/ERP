using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restmium.ERP.Services.Warehouse.Domain.Events
{
    public class IssueSlipStatusChangedToDispatchedDomainEvent
    {
        public IssueSlipStatusChangedToDispatchedDomainEvent(IssueSlip issueSlip)
        {
            this.IssueSlip = issueSlip;
        }

        public IssueSlip IssueSlip { get; }
    }
}
