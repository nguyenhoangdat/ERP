using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class UpdateSectionCommand : IRequest<Section>
    {
        public UpdateSectionCommand(UpdateSectionCommandModel model)
        {
            this.Model = model;
        }
        public UpdateSectionCommand(int id, string name) : this(new UpdateSectionCommandModel(id, name)) { }

        public UpdateSectionCommandModel Model { get; }

        public class UpdateSectionCommandModel
        {
            public UpdateSectionCommandModel(int id, string name)
            {
                this.Id = id;
                this.Name = name;
            }

            public int Id { get; }
            public string Name { get; }
        }
    }
}
