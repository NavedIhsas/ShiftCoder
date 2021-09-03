using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_FrameWork.Application;
using CommentManagement.Domain.CourseCommentAgg;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Comment;

namespace ShiftCoderQuery.Query
{
   public class CommandQuery:ICommentQuery
   {
       private readonly CommentContext _context;

       public CommandQuery(CommentContext context)
       {
           _context = context;
       }
       public List<CommentQueryModel> GetAll()
       {
           return _context.Comments
               .Include(x => x.Children).
               ThenInclude(x => x.Parent)
               .Where(x=>x.Type==1).Select(x => new CommentQueryModel()
           {
               Id = x.Parent.Id,
               Name = x.Parent.Name,
               Message = x.Parent.Message,
               Email = x.Parent.Email,
               ParentId = x.Parent.ParentId,
               ParentName = x.Parent.Parent.Name,
               OwnerRecordId = x.Parent.OwnerRecordId,
               SubComment = MapChildren(x.Children),
                   CreationDate = x.Parent.CreationDate.ToFarsi()
           }).AsNoTracking().ToList();
       }

       private static List<CommentQueryModel> MapChildren(IEnumerable<Comment> children)
       {
           return children.Select(x => new CommentQueryModel()
           {
               Name = x.Name,
               Message = x.Message,
               Email = x.Email,
               ParentId = x.ParentId,
               ParentName = x.Parent.Name,
           }).ToList();
       }
        public OperationResult Create(CommentQueryModel command)
        {
            var operation = new OperationResult();
            var comment= _context.Comments.Select(x => new CommentQueryModel()
            {
                Id = x.Id,
                Name = x.Name,
                Message = x.Message,
                Email = x.Email,
            }).AsNoTracking();

            return operation.Succeeded("نظر شما با موفقیت ثبت شد");
        }
    }
}
