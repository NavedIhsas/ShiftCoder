using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using CommentManagement.Application.Contract.HomePhoto;
using CommentManagement.Domain.SliderAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EfCore.Repository
{
   public class SliderRepository:RepositoryBase<long,Slider>,ISliderRepository
   {
       private readonly CommentContext _context;
        public SliderRepository(CommentContext dbContext, CommentContext context) : base(dbContext)
        {
            _context = context;
        }

       public List<SliderViewModel> GetAll()
       {
           return _context.Sliders.Select(x => new SliderViewModel
           {
               PictureStringFormat = x.Picture,
               PictureAlt = x.PictureAlt,
               PictureTitle = x.PictureTitle,
               ButtonText = x.ButtonText,
               CreationDate=x.CreationDate,
               Id = x.Id
           }).AsNoTracking().OrderByDescending(x => x.CreationDate).ToList();
       }

        public SliderViewModel GetDetails(long id)
        {
            return _context.Sliders.Select(x => new SliderViewModel
            {
                PictureStringFormat = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ButtonText = x.ButtonText,
                CreationDate = x.CreationDate,
                Id = x.Id,
                Title = x.Title,
                ShortTitle = x.ShortTitle,
            }).FirstOrDefault(x=>x.Id==id);
        }
    }
}
