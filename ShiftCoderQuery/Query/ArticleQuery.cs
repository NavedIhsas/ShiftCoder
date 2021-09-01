using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Article;

namespace ShiftCoderQuery.Query
{
   public class ArticleQuery:IArticleQuery
   {
       private readonly BlogContext _context;

       public ArticleQuery(BlogContext context)
       {
           _context = context;
       }

       public List<LatestArticleQueryModel> LatestArticle()
       {
        return  _context.Articles.Where(x=>x.IsPublish).Select(x => new LatestArticleQueryModel()
           {
               Title = x.Title,
               Picture = x.Picture,
               PictureAtl = x.PictureAtl,
               PictureTitle = x.PictureTitle,
               ShortDescription = x.ShortDescription,
               CreationDate=x.CreationDate,
               Slug = x.Slug,
               Keywords = x.Keywords,
               MetaDescription = x.MetaDescription
           }).AsNoTracking().OrderByDescending(x=>x.CreationDate).Take(6).ToList();

          
       }

       public List<GetAllArticleQueryModel> GetAllArticles()
       {
           return _context.Articles.Where(x=>x.IsPublish).Select(x => new GetAllArticleQueryModel()
           {
               Title = x.Title,
               Picture = x.Picture,
               PictureAtl = x.PictureAtl,
               PictureTitle = x.PictureTitle,
               ShortDescription = x.ShortDescription,
               CreationDate = x.CreationDate,
               Slug = x.Slug,
               Keywords = x.Keywords,
               MetaDescription = x.MetaDescription
           }).AsNoTracking().OrderByDescending(x => x.CreationDate).ToList();

        }
    }
}
