using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using _0_Framework.Application;
using _0_FrameWork.Application;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleCategoryRepository _articleCategory;
        private readonly IFileUploader _fileUploader;

        private readonly IArticleRepository _repository;

        public ArticleApplication(IArticleRepository repository, IFileUploader fileUploader,
            IArticleCategoryRepository articleCategory)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _articleCategory = articleCategory;
        }

        public OperationResult Create(CreateArticleViewModel command)
        {
            var operation = new OperationResult();

            DateTime? publish;
            if (command.IsPublish)
                publish = DateTime.Now;
            else
                publish = null;


          

            var categoryName = _articleCategory.GetArticleCategoryName(command.CategoryId);
            var path = $"مقاله-ها /{categoryName.Slugify()}";
            var fileName = _fileUploader.Uploader(command.Picture, path);


            var article = new Article(command.Title, command.Description, fileName, command.PictureTitle,
                command.PictureAtl, command.Slug.Slugify()
                , command.Keywords, command.CanonicalAddress, publish, command.CategoryId,
                command.MetaDescription, command.ShortDescription, command.ShowOrder, command.IsPublish,
                command.BloggerId);

            _repository.Create(article);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticleViewModel command)
        {
            var operation = new OperationResult();


            var isPublish = command.IsPublish;
            var publishStatus = _repository.GetPublishStatus(command.Id);
            var oldPublishDate = _repository.GetPublishDate(command.Id);

            var publish = oldPublishDate;

            if (publishStatus == false)
                if (command.IsPublish)
                {
                    if (oldPublishDate == null)
                    {
                        publish = DateTime.Now;
                    }
                    else
                    {
                        isPublish = command.IsPublish;
                        publish = oldPublishDate;
                    }
                }

            var article = _repository.GetById(command.Id);

            if (command.Picture != null)
            {
                var deletePath = $"wwwroot/FileUploader/{article.Picture}";
                if (File.Exists(deletePath))
                    File.Delete(deletePath);
            }

            if (article == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            var categoryName = _articleCategory.GetArticleCategoryName(command.CategoryId);
            var path = $"Articles//{categoryName.Slugify()}";
            var fileName = _fileUploader.Uploader(command.Picture, path);



            article.Edit(command.Title, command.Description, fileName, command.PictureTitle,
                command.PictureAtl, command.Slug.Slugify()
                , command.Keywords, command.CanonicalAddress, publish, command.CategoryId,
                command.MetaDescription, command.ShortDescription, command.ShowOrder, isPublish, command.BloggerId);

            _repository.Update(article);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditArticleViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel search)
        {
            return _repository.Search(search);
        }
    }
}