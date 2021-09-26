using System.Collections.Generic;
using _0_FrameWork.Application;
using CommentManagement.Application.Contract.HomePhoto;
using CommentManagement.Domain.HomePageDetailsAgg;

namespace CommentManagement.Application
{
   public class HomePhotoApplication:IHomePhotoApplication
   {
       private readonly IHomePhotoRepository _repository;
       private readonly IFileUploader _fileUploader;

       public HomePhotoApplication(IHomePhotoRepository repository, IFileUploader fileUploader)
       {
           _repository = repository;
           _fileUploader = fileUploader;
       }

       public OperationResult Create(HomePhotoViewModel command)
       {
           var operation = new OperationResult();

           var fileName = _fileUploader.Uploader(command.Picture, "/HomePhoto");
           var create = new HomePhoto(fileName, command.PictureAlt, command.PictureTitle, command.ButtonsColor);
            _repository.Create(create);
            _repository.SaveChanges();
            return operation.Succeeded();
       }

        public OperationResult Edit(HomePhotoViewModel command)
        {
            var operation = new OperationResult();

            var fileName = _fileUploader.Uploader(command.Picture, "FileUploader/HomePhoto");
           var getPhoto = _repository.GetById(command.Id);
           if (getPhoto == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            getPhoto.Edit(fileName, command.PictureAlt, command.PictureTitle, command.ButtonsColor);
            _repository.Update(getPhoto);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public List<HomePhotoViewModel> GetAllList()
        {
           return _repository.GetAll();
        }

        public HomePhotoViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }
    }
}
