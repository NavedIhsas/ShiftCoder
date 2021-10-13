using System;
using System.IO;
using _0_Framework.Application;
using _0_FrameWork.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace WebHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string ThumpPath(IFormFile file, string path)
        {
            var pathDirectory = $"{_webHostEnvironment.WebRootPath}//FileUploader//{path}";
            if (!Directory.Exists(pathDirectory))
                Directory.CreateDirectory(pathDirectory);

            if (file == null) return null;

            var fileName = $"{DateTime.Now.Date.ToFileName()}-{file.FileName}";
            var filePath = $"{pathDirectory}//{fileName}";
            using var stream = File.Create(filePath);
            return $"{path}/{fileName}";
        }
        public string Uploader(IFormFile file, string path)
        {
            var pathDirectory = $"{_webHostEnvironment.WebRootPath}//FileUploader//{path}";
            if (!Directory.Exists(pathDirectory))
                Directory.CreateDirectory(pathDirectory);

            if (file == null) return null;

            var fileName = $"{DateTime.Now.Date.ToFileName()}-{file.FileName}";
            var filePath = $"{pathDirectory}//{fileName}";

            using var stream = File.Create(filePath);
            file.CopyTo(stream);
            return $"{path}/{fileName}";
        }
    }

    public class EpisodeUploadFile : IEpisodeFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EpisodeUploadFile(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Uploader(IFormFile file, string path)
        {
            var pathDirectory = $"{_webHostEnvironment.WebRootPath}//FileUploader//{path}";
            if (!Directory.Exists(pathDirectory))
                Directory.CreateDirectory(pathDirectory);

            if (file == null) return null;

            var fileName = file.FileName;
            var filePath = $"{pathDirectory}//{fileName}";

            using var stream = File.Create(filePath);
            file.CopyTo(stream);
            return fileName;
        }
    }
}