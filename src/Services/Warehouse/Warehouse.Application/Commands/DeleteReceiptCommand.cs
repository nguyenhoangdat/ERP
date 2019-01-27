using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteReceiptCommand : IRequest<Receipt>
    {
        public DeleteReceiptCommand(DeleteReceiptCommandModel model)
        {
            this.Model = model;
        }
        public DeleteReceiptCommand(long id)
            : this(new DeleteReceiptCommandModel(id)) { }

        public DeleteReceiptCommandModel Model { get; }

        public class DeleteReceiptCommandModel
        {
            public DeleteReceiptCommandModel(long id)
            {
                this.Id = id;
            }

            public long Id { get; }
        }
    }
}
