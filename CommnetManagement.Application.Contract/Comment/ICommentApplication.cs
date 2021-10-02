using System.Collections.Generic;
using _0_FrameWork.Application;

namespace CommentManagement.Application.Contract.Comment
{
    public interface ICommentApplication
    {
        OperationResult Create(CreateCommentViewModel command);
        OperationResult IsConfirm(long id);
        OperationResult IsCancel(long id);
        List<CommentViewModel> Search(SearchCommentViewModel searchModel);
    }
}