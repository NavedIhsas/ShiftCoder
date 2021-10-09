using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using CommentManagement.Domain.VisitAgg;

namespace CommentManagement.Infrastructure.EfCore.Repository
{
    public class VisitRepository : RepositoryBase<long, Visit>, IVisitRepository
    {
        private readonly CommentContext _context;

        public VisitRepository(CommentContext dbContext, CommentContext context) : base(dbContext)
        {
            _context = context;
        }

        public Visit GetVisitBy(string ipAddress, int type, long recordOwnerId)
        {
            return _context.Visits.FirstOrDefault(x =>
                x.IpAddress == ipAddress && x.Type == type && x.RecordOwnerId == recordOwnerId);
        }


        public int? GetNumberOfVisit(int type, long ownerId)
        {
            return _context.Visits.FirstOrDefault(x => x.Type == type && x.RecordOwnerId == ownerId)?.NumberOfVisit;
        }

        public Visit GetVisitBy(string ipAddress)
        {
            return _context.Visits.OrderBy(x => x.CreationDate)
                .LastOrDefault(x => x.IpAddress == ipAddress && x.Type == ThisType.AdminPanelIndex);
        }

        public Visit GetVisitForShowQuestion(string ipAddress,int type)
        {
            return _context.Visits .FirstOrDefault(x => x.IpAddress == ipAddress &&x.Type==type);
               
        }

        public List<Visit> GetAllVisit()
        {
            return _context.Visits.ToList();
        }
    }
}