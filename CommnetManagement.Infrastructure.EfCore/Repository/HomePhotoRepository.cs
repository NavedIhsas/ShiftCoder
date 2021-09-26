using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using CommentManagement.Application.Contract.HomePhoto;
using CommentManagement.Domain.HomePageDetailsAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EfCore.Repository
{
   public class HomePhotoRepository:RepositoryBase<long,HomePhoto>,IHomePhotoRepository
   {
       private readonly CommentContext _context;
        public HomePhotoRepository(CommentContext dbContext, CommentContext context) : base(dbContext)
        {
            _context = context;
        }

       public List<HomePhotoViewModel> GetAll()
       {
           return _context.HomePhotos.Select(x => new HomePhotoViewModel
           {
               PictureStringFormat = x.Picture,
               PictureAlt = x.PictureAlt,
               PictureTitle = x.PictureTitle,
               ButtonsColor = x.ButtonsColor,
               CreationDate=x.CreationDate,
               Id = x.Id
           }).AsNoTracking().OrderByDescending(x => x.CreationDate).ToList();
       }

        public HomePhotoViewModel GetDetails(long id)
        {
            return _context.HomePhotos.Select(x => new HomePhotoViewModel
            {
                PictureStringFormat = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ButtonsColor = x.ButtonsColor,
                CreationDate = x.CreationDate,
                Id = x.Id
            }).FirstOrDefault(x=>x.Id==id);
        }
    }
}
