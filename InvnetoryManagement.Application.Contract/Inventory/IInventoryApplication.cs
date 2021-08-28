using System.Collections.Generic;
using _0_FrameWork.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventoryViewModel command);
        OperationResult Edit(EditInventoryViewModel command);
        EditInventoryViewModel GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        OperationResult IncreaseInventory(IncreaseViewModel increase);
        OperationResult ReduceInventory(ReduceViewModel reduce);
        OperationResult OperationInventory(InventoryOperationViewModel command);
    }
}