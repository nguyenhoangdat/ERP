using MediatR;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindWarehouseByIdCommand : IRequest<Domain.Entities.Warehouse>
    {
        public FindWarehouseByIdCommand(int id)
        {
            if (id <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            this.Id = id;
        }

        public int Id { get; }
    }
}
