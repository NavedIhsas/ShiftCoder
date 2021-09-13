using _0_FrameWork.Domain;

namespace CommentManagement.Domain.VisitAgg
{
    public interface IVisitRepository:IRepository<long,Visit>
    {
        Visit GetUsedBy(string ipAddress, int type,long recordOwnerId);
        int? GetNumberOfVisit(int type, long ownerId);
    }
}