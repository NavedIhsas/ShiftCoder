using BlogManagement.Application.Contract.ArticleCategory;
using System;
using System.Collections.Generic;
using _0_FrameWork.Application;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication:IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _repository;

        public ArticleCategoryApplication(IArticleCategoryRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateArticleCategoryViewModel command)
        {
            var operation = new OperationResult();

            var articleCategory = new ArticleCategory(command.Name, command.Keyword, command.MetaDescription,
                command.ShowOrder, command.CanonicalAddress);

            _repository.Create(articleCategory);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticleCategoryViewModel command)
        {
            var operation = new OperationResult();

            var articleCategory =_repository.GetById(command.Id);
                if (articleCategory==null) return operation.Failed(ApplicationMessage.RecordNotFount);
              
                articleCategory.Edit(command.Name, command.Keyword, command.MetaDescription,
                command.ShowOrder, command.CanonicalAddress);
           
                _repository.Update(articleCategory);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditArticleCategoryViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel search)
        {
           return _repository.Search(search);
        }

        public List<ArticleCategoryViewModel> SelectList()
        {
            return _repository.SelectList();
        }
    }
}
