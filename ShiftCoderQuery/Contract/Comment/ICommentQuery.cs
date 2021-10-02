using System.Collections.Generic;
using _0_FrameWork.Application;
using CommentManagement.Domain.SliderAgg;

namespace ShiftCoderQuery.Contract.Comment
{
    public interface ICommentQuery
    {
        List<CommentQueryModel> GetAllCommentCourse();
        List<CommentManagement.Domain.CourseCommentAgg.Comment> GetAll();
        List<CommentModelForUserPanel> GetUserComment(string email);
        OperationResult Create(CommentQueryModel command);
        List<Slider> GetThreeSlider();
    }
}