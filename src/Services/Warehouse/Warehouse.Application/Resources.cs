using System.Collections.Generic;

namespace Restmium.ERP.Services.Warehouse.Application
{
    public static class Resources
    {
        public static class Exceptions
        {
            static Exceptions()
            {
                Values.Add("IssueSlip_Delete_EntityNotFoundException", "Unable to delete IssueSlip(Id={0}). IssueSlip not found!");
                Values.Add("IssueSlip_EntityNotFoundException", "IssueSlip(Id={0}) not found!");
                Values.Add("IssueSlip_Update_EntityNotFoundException", "Unable to update IssueSlip(Id={0}). IssueSlip not found!");

                Values.Add("IssueSlipItem_EntityNotFoundException", "IssueSlip.Item(IssueSlipId={0}, PositionId={1}, WareId={2}) not found!");
                Values.Add("IssueSlipItem_EntitiesNotFoundException", "Unable to find entities for IssueSlip.Item(IssueSlipId={0}, WareId={1})!");

                Values.Add("IssueSlipItem_FullyAssignedException", "IssueSlip.Item(IssueSlipId={0}, WareId={1}) has been fully assigned!");
                Values.Add("IssueSlipItem_PositionAlreadyAssignedException", "IssueSlip.Item(IssueSlipId={0}, WareId={2}) is already assigned to Position(Id={1})!");
                Values.Add("IssueSlipItem_PositionAvailableUnitsException", "Cannot issue units for IssueSlip.Item(IssueSlipId={0}, PositionId={1}, WareId={2}). Position doesn't hold enough units!");
                Values.Add("IssueSlipItem_PositionWareConflictException", "IssueSlip.Item(IssueSlipId={0}, WareId={2}) cannot be assigned to Position(Id={1})! Position holds a different Ware!");
                Values.Add("IssueSlipItem_RequestedUnitsExceededException", "Cannot issue units for IssueSlip.Item(IssueSlipId={0}, PositionId={1}, WareId={2}). Requested units exceeded!");

                Values.Add("Movement_Create_PositionEmptyException", "Unable to create Movement to retrieve Ware with Id={1} at Position with Id={2}. Position doesn't hold enough units!");
                Values.Add("Movement_Create_PositionLoadCapacityException", "Unable to store Ware ({0}) at Position ({1}) in amount of {2} units. Load capacity exceeded.");
                Values.Add("Movement_Create_PositionSpaceCapacityException", "Unable to store Ware ({0}) at Position ({1}) in amount of {2} units. Space capacity exceeded.");
                Values.Add("Movement_Create_PositionWareConflictException", "Unable to create Movement to {0} Ware with Id={1} at Position with Id={2}. Position contains another Ware with Id={3}!");
                Values.Add("Movement_Delete_EntityNotFoundException", "Unable to delete Movement(Id={0}). Movement not found!");

                Values.Add("Position_Delete_EntityNotFoundException", "Unable to delete Position(Id={0}). Position not found!");
                Values.Add("Position_EntityNotFoundException", "Position(Id={0}) not found!");
                Values.Add("Position_Update_EntityNotFoundException", "Unable to update Position(Id={0}). Position not found!");

                Values.Add("Receipt_Delete_EntityNotFoundException", "Unable to delete Receipt(Id={0}). Receipt not found!");
                Values.Add("Receipt_EntityNotFoundException", "Receipt(Id={0}) not found!");
                Values.Add("Receipt_Update_EntityNotFoundException", "Unable to update Receipt(Id={0}). Receipt not found!");

                Values.Add("ReceiptItem_Create_Ware_EntityNotFoundException", "Unable to create Receipt.Item(Ware(ProductId={0}), Units={1}). Ware not found!");
                Values.Add("ReceiptItem_Create_WarehouseFullException", "Unable to process Receipt.Item(ReceiptId={0}, WareId={1}). Warehouse is full!");
                Values.Add("ReceiptItem_EntitiesNotFoundException", "Unable to find entities of Receipt.Item(ReceiptId={0}, WareId={1})!");
                Values.Add("ReceiptItem_EntityNotFoundException", "Receipt.Item(ReceiptId={0}, PositionId={1}, WareId={2}) not found!");
                Values.Add("ReceiptItem_FullyAssignedException", "Receipt.Item(ReceiptId={0}, WareId={1}) has been fully assigned!");
                Values.Add("ReceiptItem_PositionAlreadyAssignedException", "Receipt.Item(ReceiptId={0}, WareId={2}) is already assigned to Position(Id={1})!");
                Values.Add("ReceiptItem_PositionWareConflictException", "Receipt.Item(ReceiptId={0}, WareId={2}) cannot be assigned to Position(Id={1})! Position holds a different Ware!");
                Values.Add("ReceiptItem_Update_EntityNotFoundException", "Unable to update Receipt.Item(ReceiptId={0}, WareId={1}). Receipt.Item not found!");

                Values.Add("Relocation_PositionEmptyException", "Unable to relocate Wares from Position(Id={0}). Position is empty!");
                Values.Add("Relocation_PositionLoadCapacityException", "Unable to relocate Wares from Position(Id={0}) to Position(Id={1}). Load capacity exceeded!");
                Values.Add("Relocation_PositionWareConflictException", "Unable to relocate Wares from Position(Id={0}) to Position(Id={1}). Positions store different Wares.");

                Values.Add("Section_Delete_EntityNotFoundException", "Unable to delete Section(Id={0}) not found!");
                Values.Add("Section_EntityNotFoundException", "Section(Id={0}) not found!");
                Values.Add("Section_Update_EntityNotFoundException", "Unable to update Section(Id={0}) not found!");

                Values.Add("StockTaking_EntityNotFoundException", "StockTaking(Id={0}) not found!");
                Values.Add("StockTaking_Name_Section", "Stock-Taking in Section(Id={0})");
                Values.Add("StockTaking_Name_Warehouse", "Stock-Taking in Warehouse(Id={0})");

                Values.Add("StockTakingItem_EntityNotFoundException", "StockTaking.Item(StockTakingId={0}, PositionId={1}) not found!");

                Values.Add("Ware_Delete_EntityNotFoundException", "Unable to delete Ware(Id={0}). Ware not found!");
                Values.Add("Ware_EntityAlreadyExitsException", "Unable to create Ware(ProductId={0}). Ware already exists!");
                Values.Add("Ware_EntityNotFoundException", "Ware(Id={0}) not found!");
                Values.Add("Ware_ProductId_EntityNotFoundException", "Ware(ProductId={0}) not found!");
                Values.Add("Ware_Update_EntityNotFoundException", "Unable to update Ware(Id={0}). Ware not found!");

                Values.Add("Warehouse_Delete_EntityNotFoundException", "Unable to delete Warehouse(Id={0}). Warehouse not found!");
                Values.Add("Warehouse_EntityNotFoundException", "Warehouse(Id={0}) not found!");
                Values.Add("Warehouse_Update_EntityNotFoundException", "Unable to update Warehouse(Id={0}). Warehouse not found!");
            }

            public static Dictionary<string, string> Values { get; } = new Dictionary<string, string>();
        }
        public static class Naming
        {
            static Naming()
            {
                Values.Add("", "");
            }

            public static Dictionary<string, string> Values { get; } = new Dictionary<string, string>();
        }
    }
}
