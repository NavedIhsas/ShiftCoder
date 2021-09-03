using CommentManagement.Application.Contract.Comment;
using System;
using System.Collections.Generic;
using _0_FrameWork.Application;
using CommentManagement.Domain.CourseCommentAgg;

namespace CommentManagement.Application
{
    public class CommentApplication:ICommentApplication
    {
        private readonly ICommentRepository _repository;

        public CommentApplication(ICommentRepository repository)
        {
            _repository = repository;
        }
        public OperationResult Create(CreateCommentViewModel command)
        {
            var operation = new OperationResult();
          
            var comment = new Comment(command.Name, command.Email, command.Message, command.OwnerRecordId
                , command.Type, command.ParentId);

            _repository.Create(comment);
            _repository.SaveChanges();
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
