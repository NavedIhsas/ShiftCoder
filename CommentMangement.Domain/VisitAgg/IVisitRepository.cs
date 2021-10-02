using System.Collections.Generic;
using _0_FrameWork.Domain;

namespace CommentManagement.Domain.VisitAgg
{
    public interface IVisitRepository : IRepository<long, Visit>
    {
        Visit GetVisitBy(string ipAddress, int type, long recordOwnerId);
        int? GetNumberOfVisit(int type, long ownerId);
        Visit GetVisitBy(string ipAddress);
        List<Visit> GetAllVisit();
    }
}