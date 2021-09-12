using _0_FrameWork.Application;

namespace OrderApplication.Contract.Order
{
   public class OrderViewModel
    {
        public long AccountId { get; set; }
        public bool IsFinally { get; set; }
        public int OrderSum { get; set; }
    }

   public class CreateOrderViewModel
   {
       public long AccountId { get; set; }
       public bool IsFinally { get; set; }
       public int OrderSum { get; set; }
   }

   public interface IOrderApplication
   {
       OperationResult Create(string email, int courseId);
   }
}
