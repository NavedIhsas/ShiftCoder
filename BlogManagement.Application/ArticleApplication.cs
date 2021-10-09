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

            ////resize image
           
                //resize to 500 X 600
                #region 600 X 400

                var image = Image.FromStream(command.Picture.OpenReadStream());
                var resized = new Bitmap(image, new Size(500, 600));

                using var imageStream = new MemoryStream();
                resized.Save(imageStream, ImageFormat.Jpeg);
                var imageBytes = imageStream.ToArray();

                var imgName = $"{DateTime.Now.ToFileName()}-{command.Picture.FileName}";
                var path1 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/Thumb/", imgName);

                using var streamImg = new FileStream(
                    path1, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
                streamImg.Write(imageBytes, 0, imageBytes.Length);

                #endregion

                //resize to 80 X 80
                #region 80 x 80

                var img = Image.FromStream(command.Picture.OpenReadStream());
                var resizedImg = new Bitmap(img, new Size(80, 80));

                using var imgStream = new MemoryStream();
                resizedImg.Save(imgStream, ImageFormat.Jpeg);
                var imgBytes = imgStream.ToArray();

                var imageName = $"{DateTime.Now.ToFileName()}-{command.Picture.FileName}";
                var imgPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/Thumb/80X80", imageName);

                using var stream = new FileStream(
                    imgPath, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
                stream.Write(imgBytes, 0, imgBytes.Length);

                #endregion
         

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

            //delete current picture
            if (command.Picture != null)
            {
                var deletePath = $"wwwroot/FileUploader/{article.Picture}";
                if (File.Exists(deletePath))
                    File.Delete(deletePath);
            }

            //resize img
            if (command.Picture != null)
            {
                //resize to 500 X 600
                #region 600 X 400

                var image = Image.FromStream(command.Picture.OpenReadStream());
                var resized = new Bitmap(image, new Size(500, 600));

                using var imageStream = new MemoryStream();
                resized.Save(imageStream, ImageFormat.Jpeg);
                var imageBytes = imageStream.ToArray();

                var imgName = $"{DateTime.Now.ToFileName()}-{command.Picture.FileName}";
                var path1 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/Thumb/", imgName);

                using var streamImg = new FileStream(
                    path1, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
                streamImg.Write(imageBytes, 0, imageBytes.Length);

                #endregion

                //resize to 80 X 80
                #region 80 x 80

                var img = Image.FromStream(command.Picture.OpenReadStream());
                var resizedImg = new Bitmap(img, new Size(80, 80));

                using var imgStream = new MemoryStream();
                resizedImg.Save(imgStream, ImageFormat.Jpeg);
                var imgBytes = imgStream.ToArray();

                var imageName = $"{DateTime.Now.ToFileName()}-{command.Picture.FileName}";
                var imgPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/Thumb/80X80", imageName);

                using var stream = new FileStream(
                    imgPath, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
                stream.Write(imgBytes, 0, imgBytes.Length);

                #endregion
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