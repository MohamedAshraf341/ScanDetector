using Microsoft.AspNetCore.Http;

namespace ScanDetector.Service.Abstracts
{
    public interface IEmailService
    {
        Task SendEmailFromSystemAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null);

        Task<string> SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null);
    }
}
