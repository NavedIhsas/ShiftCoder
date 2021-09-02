using System.Collections.Generic;
using _0_FrameWork.Application;
using ShiftCoderQuery.Query;

namespace ShiftCoderQuery.Contract.Comment
{
  public  interface ICommentQuery
  {
       List<CommentQueryModel> GetAll();
       OperationResult Create(CommentQueryModel command);
  }
}
