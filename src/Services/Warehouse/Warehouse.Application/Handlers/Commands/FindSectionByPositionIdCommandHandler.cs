﻿using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindSectionByPositionIdCommandHandler : IRequestHandler<FindSectionByPositionIdCommand, Section>
    {
        public FindSectionByPositionIdCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Section> Handle(FindSectionByPositionIdCommand request, CancellationToken cancellationToken)
        {
            Position position = await this.DatabaseContext.Positions.FindAsync(new object[] { request.PositionId }, cancellationToken);

            if (position == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Position_EntityNotFoundException"], request.PositionId));
            }

            return position.Section;
        }
    }
}