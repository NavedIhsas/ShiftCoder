using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _context;

        public ArticleCategoryRepository(BlogContext dbContext, BlogContext context) : base(dbContext)
        {
            _context = context;
        }

        public EditArticleCategoryViewModel GetDetails(long id)
        {
            return _context.ArticleCategories.Select(x => new EditArticleCategoryViewModel
            {
                Name = x.Name,
                Keyword = x.Keyword,
                MetaDescription = x.MetaDescription,
                ShowOrder = x.ShowOrder,
                CanonicalAddress = x.CanonicalAddress,
                Id = x.Id
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel search)
        {
            var query = _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Name = x.Name,
                Id = x.Id,
                ArticleCount = x.Articles.Count,
                ShowOrder = x.ShowOrder,
                CreationDate = x.CreationDate.ToFarsi()
            }).AsNoTracking().ToList();

            if (!string.IsNullOrWhiteSpace(search.Name))
                query = query.Where(x => x.Name.Contains(search.Name)).ToList();

            var orderly = query.OrderByDescending(x => x.Id).ToList();
            return orderly;
        }

        public List<ArticleCategoryViewModel> SelectList()
        {
            return _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public string GetArticleCategoryName(long articleCategoryId)
        {
            return _context.ArticleCategories.Select(x => new { x.Name, x.Id })
                .FirstOrDefault(x => x.Id == articleCategoryId)
                ?.Name;
        }
    }
}