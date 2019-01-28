﻿using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class FindWaresInSectionCommandHandler : IRequestHandler<FindWaresInSectionCommand, IEnumerable<Ware>>
    {
        public FindWaresInSectionCommandHandler(DatabaseContext context)
        {
            this.DatabaseContext = context;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<IEnumerable<Ware>> Handle(FindWaresInSectionCommand request, CancellationToken cancellationToken)
        {
            Section section = await this.DatabaseContext.Sections.FindAsync(new object[] { request.Model.SectionId }, cancellationToken);

            if (section == null)
            {
                throw new EntityNotFoundException(string.Format(Resources.Exceptions.Values["Section_EntityNotFoundException"], request.Model.SectionId));
            }

            return this.DatabaseContext.Positions
                .Where(x => x.SectionId == request.Model.SectionId)
                .Select(x => x.GetWare())
                .AsEnumerable();
        }
    }
}
