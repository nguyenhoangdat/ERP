using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class PositionConflictException : Exception
    {
        public PositionConflictException()
        {
        }

        public PositionConflictException(string message) : base(message)
        {
        }

        public PositionConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PositionConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
