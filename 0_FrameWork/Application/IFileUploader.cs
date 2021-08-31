using Microsoft.AspNetCore.Http;

namespace _0_FrameWork.Application
{
    public interface IFileUploader
    {
        string Uploader(IFormFile file, string path);
    }

    public interface IEpisodeFileUploader
    {
        string Uploader(IFormFile file, string path);
    }
}
