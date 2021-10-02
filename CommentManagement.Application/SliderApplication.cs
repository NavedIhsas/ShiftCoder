using System.Collections.Generic;
using System.IO;
using _0_FrameWork.Application;
using CommentManagement.Application.Contract.HomePhoto;
using CommentManagement.Domain.SliderAgg;

namespace CommentManagement.Application
{
    public class SliderApplication : ISliderApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly ISliderRepository _repository;

        public SliderApplication(ISliderRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(SliderViewModel command)
        {
            var operation = new OperationResult();

            var fileName = _fileUploader.Uploader(command.Picture, "/Slider");
            var create = new Slider(fileName, command.PictureAlt, command.PictureTitle, command.ButtonText,
                command.Title, command.ShortTitle, command.ButtonLink);
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

            //delete current photo
            if (command.Picture != null)
            {
                var deletePath = $"wwwroot/FileUploader/{getPhoto.Picture}";
                if (File.Exists(deletePath))
                    File.Delete(deletePath);
            }

            getPhoto.Edit(fileName, command.PictureAlt, command.PictureTitle, command.ButtonText, command.Title,
                command.ShortTitle, command.ButtonLink);
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