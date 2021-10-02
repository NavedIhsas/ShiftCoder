using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Domain.Account.Agg;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly ITeacherRepository _blogger;
        private readonly BlogContext _context;

        public ArticleRepository(BlogContext dbContext, BlogContext context, ITeacherRepository blogger) :
            base(dbContext)
        {
            _context = context;
            _blogger = blogger;
        }

        public EditArticleViewModel GetDetails(long id)
        {
            return _context.Articles.Select(x => new EditArticleViewModel
            {
                Title = x.Title,
                Description = x.Description,
                PictureTitle = x.PictureTitle,
                PictureAtl = x.PictureAtl,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress,
                PublishDate = x.PublishDate,
                CategoryId = x.CategoryId,
                Id = x.Id,
                ShowOrder = x.ShowOrder,
                ShortDescription = x.ShortDescription,
                IsPublish = x.IsPublish,
                PictureName = x.Picture,
                BloggerId = x.BloggerId
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel search)
        {
            var query = _context.Articles.Select(x => new ArticleViewModel
            {
                Title = x.Title,
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                CategoryId = x.CategoryId,
                CategoryName = x.ArticleCategory.Name,
                Id = x.Id,
                IsPublish = x.IsPublish,
                BloggerId = x.BloggerId,
                Slug = x.Slug,
                CreationDate = x.CreationDate.ToFarsi()
            }).AsNoTracking().ToList();

            foreach (var item in query)
            {
                var blogger = _blogger.GetBloggerBy(item.BloggerId);
                if (blogger == null) continue;
                item.BloggerName = blogger.Account.FullName;
            }

            if (!string.IsNullOrWhiteSpace(search.Title))
                query = query.Where(x => x.Title.Contains(search.Title)).ToList();

            if (search.CategoryId > 0)
                query = query.Where(x => x.CategoryId == search.CategoryId).ToList();

            var orderly = query.OrderByDescending(x => x.Id).ToList();
            return orderly;
        }

        public bool? GetPublishStatus(long articleId)
        {
            return _context.Articles.Select(x => new { x.Id, x.IsPublish }).FirstOrDefault(x => x.Id == articleId)?
                .IsPublish;
        }

        public DateTime? GetPublishDate(long articleId)
        {
            var publishDate = _context.Articles.Select(x => new { x.Id, x.PublishDate })
                .FirstOrDefault(x => x.Id == articleId)?
                .PublishDate;
            return publishDate ?? null;
        }

        public Article GetArticleBy(long articleId)
        {
            return _context.Articles.Find(articleId);
        }
    }
}