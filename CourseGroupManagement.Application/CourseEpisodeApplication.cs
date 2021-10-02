using System.Collections.Generic;
using System.IO;
using _0_FrameWork.Application;
using Shop.Management.Application.Contract.CourseEpisode;
using ShopManagement.Domain.CourseAgg;
using ShopManagement.Domain.CourseEpisodeAgg;

namespace ShopManagement.Application
{
    public class CourseEpisodeApplication : ICourseEpisodeApplication
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IEpisodeFileUploader _fileUploader;
        private readonly ICourseEpisodeRepository _repository;

        public CourseEpisodeApplication(ICourseEpisodeRepository repository, IEpisodeFileUploader fileUploader,
            ICourseRepository courseRepository)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _courseRepository = courseRepository;
        }

        public OperationResult Create(CreateCourseEpisodeViewModel command)
        {
            var operation = new OperationResult();

            if (_repository.IsExist(x => x.FileName == command.File.FileName && x.CourseId == command.CourseId))
                return operation.Failed("فایل دوره نمیتواند تکراری باشد");

            var courseSlug = _courseRepository.GetCourseSlugBy(command.CourseId);
            var pathFile = $"EpisodeFiles/{courseSlug}";
            var fileName = _fileUploader.Uploader(command.File, pathFile);

            var courseEpisode = new CourseEpisode(fileName, command.Time, command.Title, command.CourseId,
                command.IsFree);

            _repository.Create(courseEpisode);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCourseEpisodeViewModel command)
        {
            var operation = new OperationResult();

            if (_repository.IsExist(x =>
                x.FileName == command.FileName && x.CourseId == command.CourseId && x.Id != command.Id))
                return operation.Failed("فایل دوره نمیتواند تکراری باشد");

            var courseSlug = _courseRepository.GetCourseSlugBy(command.CourseId);
            var pathFile = $"EpisodeFiles/{courseSlug}";
            var fileName = _fileUploader.Uploader(command.File, pathFile);

            var courseEpisode = _repository.GetById(command.Id);

            if (command.File != null)
            {
                var deletePath = $"wwwroot/FileUploader/EpisodeFiles/{courseSlug}/{courseEpisode.FileName}";
                if (File.Exists(deletePath))
                    File.Delete(deletePath);
            }

            courseEpisode.Edit(fileName, command.Time, command.Title, command.CourseId,
                command.IsFree);

            _repository.Update(courseEpisode);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditCourseEpisodeViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<CourseEpisodeViewModel> Search(CourseEpisodeSearchModel command)
        {
            return _repository.Search(command);
        }
    }
}