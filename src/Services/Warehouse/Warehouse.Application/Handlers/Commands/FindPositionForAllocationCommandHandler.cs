using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindPositionForAllocationCommandHandler : IRequestHandler<FindPositionForAllocationCommand, Position>
    {
        public FindPositionForAllocationCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Position> Handle(FindPositionForAllocationCommand request, CancellationToken cancellationToken)
        {
            //TODO: Implement
            throw new NotImplementedException();
        }
    }
}
