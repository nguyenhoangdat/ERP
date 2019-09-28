using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindPositionByIdCommand : IRequest<Position>
    {
        public FindPositionByIdCommand(long id)
        {
            if (id <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            this.Id = id;
        }

        public long Id { get; }
    }
}
