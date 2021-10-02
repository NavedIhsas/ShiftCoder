using System.Collections.Generic;
using _0_FrameWork.Domain;
using CommentManagement.Application.Contract.HomePhoto;

namespace CommentManagement.Domain.SliderAgg
{
    public interface ISliderRepository : IRepository<long, Slider>
    {
        List<SliderViewModel> GetAll();
        SliderViewModel GetDetails(long id);
    }
}