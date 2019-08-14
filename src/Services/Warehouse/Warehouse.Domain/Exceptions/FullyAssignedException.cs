using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class FullyAssignedException : Exception
    {
        public FullyAssignedException()
        {
        }

        public FullyAssignedException(string message) : base(message)
        {
        }

        public FullyAssignedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FullyAssignedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
