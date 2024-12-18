using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace ScanDetector.Service.Abstracts
{
    public interface IPhotoCloudService
    {
        Task<ImageUploadResult> UploadImage(IFormFile formFile);
        Task<DeletionResult> DeleteImage(string publicId);
    }
}
