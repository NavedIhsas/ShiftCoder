using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Application;
using BlogManagement.Domain.ArticleAgg;
using CommentManagement.Domain.CourseCommentAgg;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Comment;
using ShopManagement.Domain.CourseAgg;

namespace ShiftCoderQuery.Query
{
    public class CommandQuery : ICommentQuery
    {
        private readonly CommentContext _context;
        private readonly ICourseRepository _course;
        private readonly IArticleRepository _article;

        public CommandQuery(CommentContext context, ICourseRepository course, IArticleRepository article)
        {
            _context = context;
            _course = course;
            _article = article;
        }
        public List<CommentQueryModel> GetAllCommentCourse()
        {
            return _context.Comments
                .Include(x => x.Children).
                ThenInclude(x => x.Parent)
                .Where(x => x.Type == ThisType.Course).Select(x => new CommentQueryModel()
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

        public List<Comment> GetAll()
        {
            return _context.Comments.AsNoTracking().ToList();

        }
        public List<CommentModelForUserPanel> GetUserComment(string email)
        {
            var comment= _context.Comments.Where(x => x.Email == email)
                .Where(x => x.IsConfirmed).Select(x => new CommentModelForUserPanel
                {
                    Message = x.Message,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ParentId = x.ParentId,
                    Id = x.Id,
                    Type = x.Type,
                    OwnerRecordId = x.OwnerRecordId,
                }).ToList();
            foreach (var item in comment)
            {
                if (item.Type == ThisType.Course)
                {
                    var course = _course.GetCourseBy(item.OwnerRecordId);
                    item.CourseName = course.Name;
                    item.CourseSlug = course.Slug;
                }
                else
                {
                    var article = _article.GetArticleBy(item.OwnerRecordId);
                    item.ArticleSlug = article.Slug;
                    item.ArticleName = article.Title;
                }
            }

            return comment;
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
            var comment = _context.Comments.Select(x => new CommentQueryModel()
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
