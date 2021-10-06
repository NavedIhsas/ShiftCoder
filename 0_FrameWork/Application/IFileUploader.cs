using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace _0_FrameWork.Application
{
    public interface IFileUploader
    {
        string Uploader(IFormFile file, string path);
        string ThumpPath(IFormFile file, string path);
    }

    public interface IEpisodeFileUploader
    {
        string Uploader(IFormFile file, string path);
    }
}