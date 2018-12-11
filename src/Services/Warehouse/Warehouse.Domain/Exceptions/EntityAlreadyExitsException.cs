using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class EntityAlreadyExitsException : Exception
    {
        public EntityAlreadyExitsException()
        {
        }

        public EntityAlreadyExitsException(string message) : base(message)
        {
        }

        public EntityAlreadyExitsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityAlreadyExitsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
