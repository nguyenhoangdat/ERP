namespace Restmium.ERP.Services.Warehouse.Application.Commands
{
    public class DeleteIssueSlipCommand
    {
        public DeleteIssueSlipCommand(DeleteIssueSlipCommandModel model)
        {
            this.Model = model;
        }

        public DeleteIssueSlipCommandModel Model { get; }

        public class DeleteIssueSlipCommandModel
        {
            public DeleteIssueSlipCommandModel(long id)
            {
                this.Id = id;
            }

            public long Id { get; }
        }
    }
}
