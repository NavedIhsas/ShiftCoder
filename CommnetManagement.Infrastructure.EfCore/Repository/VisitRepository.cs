using System.Linq;
using _0_FrameWork.Domain.Infrastructure;
using CommentManagement.Domain.VisitAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EfCore.Repository
{
   public class VisitRepository:RepositoryBase<long,Visit>,IVisitRepository
   {
       private readonly CommentContext _context;
       public VisitRepository(CommentContext dbContext, CommentContext context) : base(dbContext)
       {
           _context = context;
       }

        public Visit GetUsedBy(string ipAddress, int type,long recordOwnerId)
         =>_context.Visits.FirstOrDefault(x => x.IpAddress == ipAddress && x.Type == type && x.RecordOwnerId==recordOwnerId);
       

        public int? GetNumberOfVisit(int type, long ownerId)
            => _context.Visits.FirstOrDefault(x => x.Type == type && x.RecordOwnerId == ownerId)?.NumberOfVisit;
       
   }
}
