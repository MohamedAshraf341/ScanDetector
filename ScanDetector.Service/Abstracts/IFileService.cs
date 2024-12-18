using Microsoft.AspNetCore.Http;

namespace ScanDetector.Service.Abstracts
{
    public interface IFileService
    {
        Task<string> UploadImage(string Location, IFormFile file);
        bool DeleteImage(string imagePath);
    }
}
