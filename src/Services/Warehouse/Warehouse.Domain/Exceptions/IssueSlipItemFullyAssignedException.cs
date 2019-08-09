using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class IssueSlipItemFullyAssignedException : Exception
    {
        public IssueSlipItemFullyAssignedException()
        {
        }

        public IssueSlipItemFullyAssignedException(string message) : base(message)
        {
        }

        public IssueSlipItemFullyAssignedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IssueSlipItemFullyAssignedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
