namespace InventoryManagement.Application.Contract.Inventory
{
   public class IncreaseViewModel
    {
        public int Count { get; set; }
        public long OperationId { get; set; }
        public long InventoryId { get; set; }
        public string Description { get; set; }
    }
}
