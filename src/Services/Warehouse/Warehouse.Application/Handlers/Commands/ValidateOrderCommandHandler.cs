using MediatR;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Validators;
using System.Threading;
using System.Threading.Tasks;

namespace Restmium.ERP.Services.Warehouse.Application.Handlers.Commands
{
    public class ValidateOrderCommandHandler : IRequestHandler<ValidateOrderCommand, bool>
    {
        public ValidateOrderCommandHandler(IOrderValidator orderValidator)
        {
            this.OrderValidator = orderValidator;
        }

        public IOrderValidator OrderValidator { get; }

        public async Task<bool> Handle(ValidateOrderCommand request, CancellationToken cancellationToken)
        {
            // TODO: 2020.1 Reimplement

            bool isValid = true;

            foreach (ValidateOrderCommand.ProductCount item in request.WareCounts)
            {
                if (this.OrderValidator.IsValid(item.WareId, item.Count) == false)
                {
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}
