using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Application;
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

        public ArticleQuery(BlogContext context, CommentContext comment, IVisitRepository visit)
        {
            _context = context;
            _comment = comment;
            _visit = visit;
        }

        public List<LatestArticleQueryModel> LatestArticle()
        {
            var article= _context.Articles.Where(x => x.IsPublish).Select(x => new LatestArticleQueryModel()
            {
                Title = x.Title,
                Picture = x.Picture,
                PictureAtl = x.PictureAtl,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                CreationDate = x.CreationDate,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Id=x.Id,
            }).AsNoTracking().OrderByDescending(x => x.CreationDate).Take(6).ToList();

            foreach (var item in article)
            {
                item.VisitCount = _visit.GetNumberOfVisit(VisitType.Article, item.Id);
            }

            return article;
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
                IsPublish = x.IsPublish,
                PublishDate = x.PublishDate.ToFarsi(),
                CategoryName = x.ArticleCategory.Name
            }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (article == null) return null;


            var visit = _visit.GetUsedBy(ipAddress, VisitType.Article, article.Id);

            if (visit != null && visit.LastVisitDateTime.Date != DateTime.Now)
            {
                visit.ReduceVisit(visit.NumberOfVisit);
                visit.SetDateTime();
                _visit.SaveChanges();
            }
            else if (visit == null)
            {
                visit = new Visit(VisitType.Article, ipAddress, DateTime.Now, 1,article.Id);
                _visit.Create(visit);
                _visit.SaveChanges();
            }
            article.VisitCount = visit.NumberOfVisit;

            var comment = _comment.Comments.

               Where(x => x.Type == CommentType.Article && x.ParentId == null)
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
                   CreationDate = x.CreationDate.ToFarsi(),
                   OwnerRecordId = x.OwnerRecordId,
               }).AsNoTracking().ToList();

            foreach (var item in comment)
                MapChildren(item);

            article.Comments = comment;
            return article;

        }

        private void MapChildren(CommentQueryModel parent)
        {
            var sub = _comment.Comments
                .Where(x => x.Type == CommentType.Article && x.ParentId == parent.Id)
                .Select(x => new CommentQueryModel
                {
                    Name = x.Name,
                    Email = x.Email,
                    Message = x.Message,
                    Id = x.Id,
                    ParentName = x.Parent.Name,
                    ParentId = x.ParentId,
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
