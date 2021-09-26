using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Application;
using AccountManagement.Domain.Account.Agg;
using AccountManagement.Infrastructure.EfCore;
using BlogManagement.Infrastructure.EfCore;
using CommentManagement.Domain.VisitAgg;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Article;
using ShiftCoderQuery.Contract.Comment;

namespace ShiftCoderQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;
        private readonly CommentContext _comment;
        private readonly IVisitRepository _visit;
        private readonly ITeacherRepository _teacher;
        private readonly AccountContext _account;

        public ArticleQuery(BlogContext context, CommentContext comment, IVisitRepository visit, ITeacherRepository teacher, AccountContext account)
        {
            _context = context;
            _comment = comment;
            _visit = visit;
            _teacher = teacher;
            _account = account;
        }

        public List<LatestArticleQueryModel> LatestArticle()
        {
            var article = _context.Articles.Where(x => x.IsPublish).Select(x => new LatestArticleQueryModel()
            {
                Title = x.Title,
                Picture = x.Picture,
                PictureAtl = x.PictureAtl,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                CreationDate = x.CreationDate,
                Slug = x.Slug,
                Keywords = x.Keywords,
                BloggerId = x.BloggerId,
                MetaDescription = x.MetaDescription,
                Id = x.Id,
            }).AsNoTracking().OrderByDescending(x => x.CreationDate).Take(6).ToList();

            foreach (var item in article)
            {
                var comments = _comment.Comments.Where(x => x.OwnerRecordId == item.Id && x.Type == ThisType.Article)
                    .ToList();
                item.Comments = comments;

                var blogger = _account.Teachers.Include(x => x.Account).FirstOrDefault(x => x.Id == item.BloggerId);
                if (blogger == null) continue;
                item.BloggerName = blogger.Account.FullName;

                item.VisitCount = _visit.GetNumberOfVisit(ThisType.Article, item.Id);
            }

            return article;
        }

        public PaginationArticlesViewModel GetAllArticles(SearchArticleQueryModel search, List<long> bloggerId, List<string> categories, int pageId = 1)
        {
            var query = _context.Articles
                .Where(x => x.IsPublish).Include(x => x.ArticleCategory)
                .Select(x => new GetAllArticleQueryModel()
                {
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAtl = x.PictureAtl,
                    PictureTitle = x.PictureTitle,
                    ShortDescription = x.ShortDescription,
                    CreationDate = x.CreationDate,
                    Slug = x.Slug,
                    Id=x.Id,
                    BloggerId = x.BloggerId,
                    Keywords = x.Keywords,
                    ArticleCategory = x.ArticleCategory.Name,
                    MetaDescription = x.MetaDescription,
                }).AsNoTracking().OrderByDescending(x => x.CreationDate).ToList();



            foreach (var item in query)
            {
                var comments = _comment.Comments.Where(x => x.OwnerRecordId == item.Id && x.Type == ThisType.Article)
                    .ToList();
                item.Comments = comments;

                var blogger = _account.Teachers.Include(x => x.Account).FirstOrDefault(x => x.Id == item.BloggerId);
                if (blogger == null) continue;
                item.BloggerName = blogger.Account.FullName;

                item.VisitCount = _visit.GetNumberOfVisit(ThisType.Article, item.Id);
            }

            //---search---//
            if (!string.IsNullOrEmpty(search.Title))
                query = query.Where(x =>
                        x.Title.ToLower().Contains(search.Title.ToLower().Trim()))
                    .ToList();

            //---sort--//
            if (!string.IsNullOrWhiteSpace(search.Filter))
            {
                query = search.Filter switch
                {
                    "new" => query.OrderByDescending(x => x.CreationDate).ToList(),
                    "poplar" => query.OrderBy(x => x.CreationDate).ToList(),
                    _ => query
                };
            }

            //filter by category
            if (categories.Count != 0)
                foreach (var group in categories)
                    query = query.Where(x => x.ArticleCategory.Contains(group)).ToList();

            //filter by blogger
            if (bloggerId.Count != 0)
                foreach (var group in bloggerId)
                    query = query.Where(x => x.BloggerId == group).ToList();


            //---paging---//
            const int take = 9;
            var skip = (pageId - 1) * take;

            var list = new PaginationArticlesViewModel()
            {
                CurrentPage = pageId,
                PageCount = (int)Math.Ceiling(query.Count / (double)take),
                Articles = query.Skip(skip).Take(take).ToList()
            };
            return list;
        }

        public SinglePageArticleQueryModel GetSingleArticleBy(string slug, string ipAddress)
        {
            var article = _context.Articles.Include(x => x.ArticleCategory).Select(x => new SinglePageArticleQueryModel
            {
                Id = x.Id,
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
                BloggerId = x.BloggerId,
                IsPublish = x.IsPublish,
                PublishDate = x.PublishDate.ToFarsi(),
                CategoryName = x.ArticleCategory.Name
            }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (article == null) return null;

            #region Visit

            //start-visit
            var visit = _visit.GetVisitBy(ipAddress, ThisType.Article, article.Id);
            if (visit != null && visit.LastVisitDateTime.Date != DateTime.Now.Date)
            {
                visit.ReduceVisit(visit.NumberOfVisit);
                visit.SetDateTime();
                _visit.SaveChanges();
            }
            else if (visit == null)
            {
                visit = new Visit(ThisType.Article, ipAddress, DateTime.Now, 1, article.Id);
                _visit.Create(visit);
                _visit.SaveChanges();
            }
            article.VisitCount = visit.NumberOfVisit;

            #endregion

            #region Comment

            //start-Comment//
            var comment = _comment.Comments.
                Where(x => x.Type == ThisType.Article && x.ParentId == null)
                .Where(x => x.OwnerRecordId == article.Id)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new CommentQueryModel
                {
                    Name = x.Name,
                    Email = x.Email,
                    Message = x.Message,
                    Id = x.Id,
                    ParentName = x.Parent.Name,
                    ParentId = x.ParentId,
                    Picture = x.Picture,
                    CreateDateTime = x.CreationDate,
                    CreationDate = x.CreationDate.ToFarsi(),
                    OwnerRecordId = x.OwnerRecordId,
                }).AsNoTracking().ToList();

            foreach (var item in comment)
                MapChildren(item);

            article.Comments = comment;
            article.CommentList = _comment.Comments.
                Where(x => x.Type == ThisType.Article && x.OwnerRecordId == article.Id)
              .ToList();

            #endregion

            #region Blogger
            //start-blogger//
            var blogger = _teacher.GetBloggerBy(article.BloggerId);
            if (blogger == null) return article;
            article.BloggerName = blogger.Account.FullName;
            article.BloggerBio = blogger.Bio;
            article.BloggerResume = blogger.Resumes;
            article.BloggerSkills = blogger.Skills;
            article.BloggerPicture = blogger.Account.Avatar;

            article.BloggerArticlesList = _context.Articles
                .Where(x => x.BloggerId == blogger.Id)
                .Select(selector: x => new { x.Title, x.Slug })
                .Select(x => new BloggerArticlesViewModel()
                {
                    Title = x.Title,
                    Slug = x.Slug
                }).ToList();

            #endregion

            return article;
        }

        private void MapChildren(CommentQueryModel parent)
        {
            var sub = _comment.Comments
                .OrderByDescending(x => x.CreationDate).Where(x => x.Type == ThisType.Article && x.ParentId == parent.Id)
                .Select(x => new CommentQueryModel
                {
                    Name = x.Name,
                    Email = x.Email,
                    Message = x.Message,
                    Id = x.Id,
                    ParentName = x.Parent.Name,
                    ParentId = x.ParentId,
                    Picture = x.Picture,
                    CreateDateTime = x.CreationDate,
                    CreationDate = x.CreationDate.ToFarsi(),
                    OwnerRecordId = x.OwnerRecordId,
                }).ToList();

            foreach (var item in sub)
            {
                MapChildren(item);
                parent.SubComment.Add(item);
            }
        }
    }
}
