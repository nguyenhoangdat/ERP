using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Entities.Extensions;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class RestoreSectionFromBinCommandHandler : IRequestHandler<RestoreSectionFromBinCommand, Section>
    {
        public RestoreSectionFromBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        public IMediator Mediator { get; }

        public async Task<Section> Handle(RestoreSectionFromBinCommand request, CancellationToken cancellationToken)
        {
            Section section = this.DatabaseContext.Sections.FirstOrDefault(x => x.Id == request.SectionId);

            if (section == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Section_EntityNotFoundException, request.SectionId));
            }
            if (section.CanBeRestoredFromBin() == false)
            {
                throw new EntityRestoreFromBinException(string.Format(Properties.Resources.Section_EntityRestoreFromBinException, request.SectionId));
            }

            section.UtcMovedToBin = null;
            section.MovedToBinInCascade = false;

            foreach (Position item in section.Positions.Where(x => x.MovedToBinInCascade))
            {
                await this.Mediator.Send(new RestorePositionFromBinCommand(item.Id), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return section;
        }
    }
}
