using System.Collections.Generic;
using System.Linq;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.ArticleCategory;

namespace ShiftCoderQuery.Query
{
   public class ArticleCategoryQuery:IArticleCategoryQuery
   {
       private readonly BlogContext _context;

       public ArticleCategoryQuery(BlogContext context)
       {
           _context = context;
       }
       public List<ArticleCategoryQueryModel> GetAll()
       {
          return _context.ArticleCategories.Select(x => new ArticleCategoryQueryModel()
           {
               Name = x.Name,
               Id = x.Id,
               ArticlesCount=x.Articles.Count
           }).AsNoTracking().ToList();
       }
    }
}
