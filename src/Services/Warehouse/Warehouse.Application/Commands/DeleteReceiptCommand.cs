using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteReceiptCommand : IRequest<Receipt>
    {
        public DeleteReceiptCommand(long id)
        {
            this.Id = id;
        }

        public long Id { get; }
    }
}
