using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class IssueSlipItemRequestedUnitsExceededException : Exception
    {
        public IssueSlipItemRequestedUnitsExceededException()
        {
        }

        public IssueSlipItemRequestedUnitsExceededException(string message) : base(message)
        {
        }

        public IssueSlipItemRequestedUnitsExceededException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IssueSlipItemRequestedUnitsExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
