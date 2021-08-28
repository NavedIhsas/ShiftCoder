using System.Collections.Generic;
using _0_FrameWork.Domain;
using InventoryManagement.Application.Contract.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository:IRepository<long,Inventory>
    {
        EditInventoryViewModel GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
    }
}
