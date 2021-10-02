using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CourseCommentAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EfCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _context;

        public CommentRepository(CommentContext dbContext, CommentContext context) : base(dbContext)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(SearchCommentViewModel searchModel)
        {
            var query = _context.Comments.Select(x => new CommentViewModel
            {
                Name = x.Name,
                Email = x.Email,
                Message = x.Message,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                OwnerRecordId = x.OwnerRecordId,
                Type = x.Type,
                Id = x.Id,
                CreationDate = x.CreationDate.ToFarsi()
            }).AsNoTracking().ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.ToLower().Trim().Contains(searchModel.Name.ToLower().Trim())).ToList();


            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.ToLower().Trim().Contains(searchModel.Email.ToLower().Trim()))
                    .ToList();

            if (searchModel.IsConfirm)
                query = query.Where(x => x.IsConfirmed).ToList();

            if (searchModel.IsWaite)
                query = query.Where(x => !x.IsConfirmed && !x.IsCanceled).ToList();

            if (searchModel.IsCancel)
                query = query.Where(x => x.IsCanceled).ToList();


            var orderly = query.OrderByDescending(x => x.Id).ToList();
            return orderly;
        }

        public List<CommentViewModel> GetCommentBy(long ownerId)
        {
            var comment = _context.Comments.Where(x => x.Id == ownerId).Select(x => new CommentViewModel
            {
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed
            }).ToList();
            return comment;
        }
    }
}