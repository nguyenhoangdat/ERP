using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class PositionWareConflictException : Exception
    {
        public PositionWareConflictException()
        {
        }

        public PositionWareConflictException(string message) : base(message)
        {
        }

        public PositionWareConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PositionWareConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
