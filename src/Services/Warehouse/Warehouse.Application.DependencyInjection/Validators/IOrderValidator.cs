namespace Restmium.ERP.Services.Warehouse.Application.DependencyInjection.Validators
{
    public interface IOrderValidator
    {
        bool IsValid(int wareId, int count);
    }
}
