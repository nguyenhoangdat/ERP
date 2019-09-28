using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using System;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateSectionCommand : IRequest<Section>
    {
        public UpdateSectionCommand(int id, string name)
        {
            if (id <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}
