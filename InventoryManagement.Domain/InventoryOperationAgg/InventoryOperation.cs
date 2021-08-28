using System;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Domain.InventoryOperationAgg
{
   public class InventoryOperation
    {
        public long Id { get; private set; }
        public bool Operation { get; private set; }
        public int Count { get; private set; }
        public long CurrentCount { get; private set; }
        public long OrderId { get; private set; }
        public long OperatorId { get; private set; }
        public long OperationId { get; private set; }
        public string Description { get; private set; }
        public DateTime OperationDate { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }

        public InventoryOperation(bool operation, int count, long currentCount, long orderId, long operationId, string description)
        {
            Operation = operation;
            Count = count;
            CurrentCount = currentCount;
            OrderId = orderId;
            
            OperationId = operationId;
            Description = description;
        }
    }
}
