using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateReceiptCommand : IRequest<Receipt>
    {
        public UpdateReceiptCommand(long id, DateTime utcExpected, DateTime? utcReceived)
        {
            this.Id = id;
            this.UtcExpected = utcExpected;
            this.UtcReceived = utcReceived;
        }

        public long Id { get; }
        public DateTime UtcExpected { get; }
        public DateTime? UtcReceived { get; }
    }
}
