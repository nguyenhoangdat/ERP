using System;
using System.Runtime.Serialization;

namespace Restmium.ERP.Services.Warehouse.Domain.Exceptions
{
    public class EntityDeleteException : Exception
    {
        public EntityDeleteException()
        {
        }

        public EntityDeleteException(string message) : base(message)
        {
        }

        public EntityDeleteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
