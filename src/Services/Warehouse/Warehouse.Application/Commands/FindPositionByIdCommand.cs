using MediatR;
using Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class FindPositionByIdCommand : IRequest<Position>
    {
        public FindPositionByIdCommand(FindPositionByIdCommandModel model)
        {
            this.Model = model;
        }
        public FindPositionByIdCommand(long id) : this(new FindPositionByIdCommandModel(id))
        {
        }

        public FindPositionByIdCommandModel Model { get; }

        public class FindPositionByIdCommandModel
        {
            public FindPositionByIdCommandModel(long id)
            {
                this.Id = id;
            }

            public long Id { get; }
        }
    }
}
