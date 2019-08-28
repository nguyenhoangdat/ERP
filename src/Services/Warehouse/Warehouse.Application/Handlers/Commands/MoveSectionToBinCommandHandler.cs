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
    public class MoveSectionToBinCommandHandler : IRequestHandler<MoveSectionToBinCommand, Section>
    {
        public MoveSectionToBinCommandHandler(DatabaseContext databaseContext, IMediator mediator)
        {
            this.DatabaseContext = databaseContext;
            this.Mediator = mediator;
        }

        protected DatabaseContext DatabaseContext { get; }
        protected IMediator Mediator { get; }

        public async Task<Section> Handle(MoveSectionToBinCommand request, CancellationToken cancellationToken)
        {
            Section section = this.DatabaseContext.Sections.FirstOrDefault(x => x.Id == request.SectionId);

            if (section == null)
            {
                throw new EntityNotFoundException(string.Format(Properties.Resources.Section_EntityNotFoundException, request.SectionId));
            }
            if (request.MovedToBinInCascade == false)
            {
                if (section.CanBeMovedToBin() == false)
                {
                    throw new EntityMoveToBinException(string.Format(Properties.Resources.Section_EntityMoveToBinException, request.SectionId));
                }
            }

            section.UtcMovedToBin = DateTime.UtcNow;
            section.MovedToBinInCascade = request.MovedToBinInCascade;

            foreach (Position item in section.Positions.Where(x => x.UtcMovedToBin == null))
            {
                await this.Mediator.Send(new MovePositionToBinCommand(item.Id, true), cancellationToken);
            }

            await this.DatabaseContext.SaveChangesAsync(cancellationToken);

            return section;
        }
    }
}
