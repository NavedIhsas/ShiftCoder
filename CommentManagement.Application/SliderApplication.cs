using System.Collections.Generic;
using _0_FrameWork.Application;
using CommentManagement.Application.Contract.HomePhoto;
using CommentManagement.Domain.SliderAgg;

namespace CommentManagement.Application
{
   public class SliderApplication:ISliderApplication
   {
       private readonly ISliderRepository _repository;
       private readonly IFileUploader _fileUploader;

       public SliderApplication(ISliderRepository repository, IFileUploader fileUploader)
       {
           _repository = repository;
           _fileUploader = fileUploader;
       }

       public OperationResult Create(SliderViewModel command)
       {
           var operation = new OperationResult();

           var fileName = _fileUploader.Uploader(command.Picture, "/Slider");
           var create = new Slider(fileName, command.PictureAlt, command.PictureTitle, command.ButtonText,command.Title,command.ShortTitle);
            _repository.Create(create);
            _repository.SaveChanges();
            return operation.Succeeded();
       }

        public OperationResult Edit(SliderViewModel command)
        {
            var operation = new OperationResult();

            var fileName = _fileUploader.Uploader(command.Picture, "/Slider");
           var getPhoto = _repository.GetById(command.Id);
           if (getPhoto == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            getPhoto.Edit(fileName, command.PictureAlt, command.PictureTitle, command.ButtonText,command.Title,command.ShortTitle);
            _repository.Update(getPhoto);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public List<SliderViewModel> GetAllList()
        {
           return _repository.GetAll();
        }

        public SliderViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }
    }
}
