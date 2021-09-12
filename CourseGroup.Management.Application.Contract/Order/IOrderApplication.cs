namespace Shop.Management.Application.Contract.Order
{
    public interface IOrderApplication
    {
        long Create(string userName, long courseId);
    }
}
