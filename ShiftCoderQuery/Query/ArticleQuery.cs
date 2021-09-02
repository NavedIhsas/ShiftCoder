using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Article;

namespace ShiftCoderQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;

        public ArticleQuery(BlogContext context)
        {
            _context = context;
        }

        public List<LatestArticleQueryModel> LatestArticle()
        {
            return _context.Articles.Where(x => x.IsPublish).Select(x => new LatestArticleQueryModel()
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
            }).AsNoTracking().OrderByDescending(x => x.CreationDate).Take(6).ToList();


        }

        public List<GetAllArticleQueryModel> GetAllArticles(SearchArticleQueryModel search)
        {
            var query = _context.Articles.Where(x => x.IsPublish).Select(x => new GetAllArticleQueryModel()
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

            if (!string.IsNullOrEmpty(search.Title))
                query = query.Where(x =>
                        x.Title.ToLower().Contains(search.Title.ToLower().Trim()))
                    .ToList();

            if (string.IsNullOrWhiteSpace(search.Filter)) return query;
            {
                query = search.Filter switch
                {
                    "new" => query.OrderByDescending(x => x.CreationDate).ToList(),
                    "poplar" => query.OrderBy(x => x.CreationDate).ToList(),
                    _ => query
                };
            }



            return query;
        }

        public SinglePageArticleQueryModel GetSingleArticleBy(string slug)
        {
           return _context.Articles.Include(x=>x.ArticleCategory).Select(x => new SinglePageArticleQueryModel
            {
                Title = x.Title,
                Description = x.Description,
                ShortDescription = x.ShortDescription,
                ShowOrder = x.ShowOrder,
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAtl = x.PictureAtl,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress,
                IsPublish = x.IsPublish,
                PublishDate = x.PublishDate.ToFarsi(),
                CategoryName = x.ArticleCategory.Name
            }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);
        }
    }
}
