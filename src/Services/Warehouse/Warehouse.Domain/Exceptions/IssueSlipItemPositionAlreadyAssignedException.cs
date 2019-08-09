using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class IssueSlipItemPositionAlreadyAssignedException : Exception
    {
        public IssueSlipItemPositionAlreadyAssignedException()
        {
        }

        public IssueSlipItemPositionAlreadyAssignedException(string message) : base(message)
        {
        }

        public IssueSlipItemPositionAlreadyAssignedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IssueSlipItemPositionAlreadyAssignedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
