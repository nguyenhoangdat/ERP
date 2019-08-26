﻿using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class MoveSectionToBinCommandHandler : IRequestHandler<MoveSectionToBinCommand, Section>
    {
        public MoveSectionToBinCommandHandler(DatabaseContext databaseContext)
        {
            this.DatabaseContext = databaseContext;
        }

        protected DatabaseContext DatabaseContext { get; }

        public async Task<Section> Handle(MoveSectionToBinCommand request, CancellationToken cancellationToken)
        {
            Section section = this.DatabaseContext.Sections.FirstOrDefault(x => x.Id == request.SectionId);

            if (section == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Section_EntityNotFoundException, request.SectionId));
            }

            section.UtcMovedToBin = DateTime.UtcNow;

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return section;
        }
    }
}
