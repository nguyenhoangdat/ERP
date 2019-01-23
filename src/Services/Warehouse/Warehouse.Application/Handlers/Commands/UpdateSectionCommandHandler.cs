﻿using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Events;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Section>
    {
        protected const string UpdateSectionCommandHandlerEntityNotFoundException = "Unable to update Section with id={0}. Section not found!";

        public UpdateSectionCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Section> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            if (!this.DatabaseContext.Sections.Any(x => x.Id == request.Model.Id))
            {
                throw new EntityNotFoundException(string.Format(UpdateSectionCommandHandlerEntityNotFoundException, request.Model.Id));
            }

            Section section = this.DatabaseContext.Sections.Find(request.Model.Id);
            section.Name = request.Model.Name;
            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            await this.Mediator.Publish(new SectionUpdatedDomainEvent(section));

            return section;
        }
    }
}