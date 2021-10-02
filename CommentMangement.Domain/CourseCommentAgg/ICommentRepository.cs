using System.Collections.Generic;
using _0_FrameWork.Domain;
using CommentManagement.Application.Contract.Comment;

namespace CommentManagement.Domain.CourseCommentAgg
{
    public interface ICommentRepository : IRepository<long, Comment>
    {
        List<CommentViewModel> Search(SearchCommentViewModel searchModel);
        List<CommentViewModel> GetCommentBy(long ownerId);
    }
}