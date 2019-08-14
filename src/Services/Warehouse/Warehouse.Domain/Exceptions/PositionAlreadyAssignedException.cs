using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class PositionAlreadyAssignedException : Exception
    {
        public PositionAlreadyAssignedException()
        {
        }

        public PositionAlreadyAssignedException(string message) : base(message)
        {
        }

        public PositionAlreadyAssignedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PositionAlreadyAssignedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
