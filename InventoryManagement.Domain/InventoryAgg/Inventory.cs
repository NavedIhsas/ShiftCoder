using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using _0_FrameWork.Domain;
using InventoryManagement.Domain.InventoryOperationAgg;

namespace InventoryManagement.Domain.InventoryAgg
{
   public class Inventory:EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> Operation { get; set; }

        public Inventory( double unitPrice, long productId)
        {
            UnitPrice = unitPrice;
            ProductId = productId;
            InStock = false;
        }

        public void Edit(double unitPrice, long productId)
        {
            UnitPrice = unitPrice;
            ProductId = productId;
        }

       public long CalculateInventoryStock()
       {
           var plus = Operation.Where(x => x.Operation).Sum(x => x.Count);
           var minus = Operation.Where(x => !x.Operation).Sum(x => x.Count);
           return plus - minus;
       }

       public void IncreaseInventory(int count,long operationId,string description)
       {
           var currentCount = CalculateInventoryStock() + count;
           var increase = new InventoryOperation(true, count, currentCount, 0, operationId, description);
            Operation.Add(increase);
            InStock = currentCount > 0;
       }

       public void ReduceInventory(int count, long operationId, long orderId, string description)
       {
           var currentCount = CalculateInventoryStock() - count;
           var reduce = new InventoryOperation(false, count, currentCount, orderId, operationId, description);
            Operation.Add(reduce);
            InStock = currentCount >= 0;
       }
    }
}
