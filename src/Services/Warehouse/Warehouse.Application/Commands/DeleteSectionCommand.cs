using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteSectionCommand : IRequest<Section>
    {
        public DeleteSectionCommand(DeleteSectionCommandModel model)
        {
            this.Model = model;
        }

        public DeleteSectionCommandModel Model { get; }

        public class DeleteSectionCommandModel
        {
            public DeleteSectionCommandModel(int id)
            {
                this.Id = id;
            }

            public int Id { get; }
        }
    }
}
