using System.Collections.Generic;
using _0_FrameWork.Domain;
using CommentManagement.Application.Contract.HomePhoto;

namespace CommentManagement.Domain.HomePageDetailsAgg
{
    public interface IHomePhotoRepository:IRepository<long,HomePhoto>
    {
        List<HomePhotoViewModel> GetAll();
        HomePhotoViewModel GetDetails(long id);
    }
}