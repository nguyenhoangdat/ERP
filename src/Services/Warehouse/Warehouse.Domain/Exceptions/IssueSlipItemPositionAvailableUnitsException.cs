using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class IssueSlipItemPositionAvailableUnitsException : Exception
    {
        public IssueSlipItemPositionAvailableUnitsException()
        {
        }

        public IssueSlipItemPositionAvailableUnitsException(string message) : base(message)
        {
        }

        public IssueSlipItemPositionAvailableUnitsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IssueSlipItemPositionAvailableUnitsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
