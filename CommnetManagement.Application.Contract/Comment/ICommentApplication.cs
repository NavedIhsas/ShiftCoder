using _0_FrameWork.Application;
using System.Collections.Generic;

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
