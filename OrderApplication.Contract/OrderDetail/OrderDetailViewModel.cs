namespace OrderApplication.Contract.OrderDetail
{
   public class OrderDetailViewModel
    {
        public long OrderId { get; set; }
        public long AccountId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }

   public class CreateOrderDetailViewModel
   {
       public long OrderId { get; set; }
       public long AccountId { get; set; }
       public int Count { get; set; }
       public double Price { get; set; }
   }

   public interface IOrderDetailApplication
    {

    }
}
