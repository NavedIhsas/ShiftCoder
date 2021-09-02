using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EfCore.Repository;
using Microsoft.Extensions.DependencyInjection;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Application;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Article;
using ShiftCoderQuery.Contract.ArticleCategory;
using ShiftCoderQuery.Query;

namespace BlogManagement.Infrastructure
{
    public class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            service.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();

            service.AddTransient<IArticleRepository, ArticleRepository>();
            service.AddTransient<IArticleApplication, ArticleApplication>();

            service.AddTransient<IArticleQuery, ArticleQuery>();
            service.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();


            service.AddDbContext<BlogContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}
