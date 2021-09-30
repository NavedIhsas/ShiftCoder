using System.Collections.Generic;
using _0_FrameWork.Application;

namespace CommentManagement.Application.Contract.HomePhoto
{
    public interface ISliderApplication
    {
        OperationResult Create(SliderViewModel command);
        OperationResult Edit(SliderViewModel command);
        List<SliderViewModel> GetAllList();
        SliderViewModel GetDetails(long id);
    }
}