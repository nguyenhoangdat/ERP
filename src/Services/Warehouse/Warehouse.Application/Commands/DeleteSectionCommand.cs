using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteSectionCommand : IRequest<Section>
    {
        public DeleteSectionCommand(int id)
        {
            this.Id = id;
        }

        public int Id { get; }
    }
}
