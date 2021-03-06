using System.Collections.Generic;
using _0_FrameWork.Application;
using BlogManagement.Domain.ArticleAgg;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CourseCommentAgg;
using CommentManagement.Domain.Notification.Agg;
using ShopManagement.Domain.CourseAgg;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly IArticleRepository _article;
        private readonly ICourseRepository _course;
        private readonly INotificationRepository _notification;
        private readonly ICommentRepository _repository;

        public CommentApplication(ICommentRepository repository, INotificationRepository notification,
            ICourseRepository course, IArticleRepository article)
        {
            _repository = repository;
            _notification = notification;
            _course = course;
            _article = article;
        }

        public OperationResult Create(CreateCommentViewModel command)
        {
            var operation = new OperationResult();

            var comment = new Comment(command.Name, command.Email, command.Message, command.OwnerRecordId
                , command.Type, command.ParentId, "");

            _repository.Create(comment);
            _repository.SaveChanges();

            if (comment.Type == 1)
            {
                var course = _course.GetCourseBy(comment.OwnerRecordId);

                var notification =
                    new Notification($"{comment.Name} نظری را در دورۀ ( {course.Name} ) ارسال کرد", ThisType.Comment,
                        comment.Id);

                _notification.Create(notification);
                _notification.SaveChanges();
            }
            else
            {
                var article = _article.GetArticleBy(comment.OwnerRecordId);
                var notification =
                    new Notification($"{comment.Name} نظری را در مقالۀ ( {article.Title} ) ارسال کرد", ThisType.Comment,
                        comment.Id);

                _notification.Create(notification);
                _notification.SaveChanges();
            }

            return operation.Succeeded();
        }

        public OperationResult IsConfirm(long id)
        {
            var operation = new OperationResult();

            var confirm = _repository.GetById(id);

            if (confirm == null) return operation.Failed(ApplicationMessage.RecordNotFount);
            confirm.IsConfirm(id);

            _repository.Update(confirm);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult IsCancel(long id)
        {
            var operation = new OperationResult();

            var cancel = _repository.GetById(id);

            if (cancel == null) return operation.Failed(ApplicationMessage.RecordNotFount);
            cancel.IsCancel(id);

            _repository.Update(cancel);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public List<CommentViewModel> Search(SearchCommentViewModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}