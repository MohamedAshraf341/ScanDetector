using ScanDetector.Data.Helpers;
using ScanDetector.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using ScanDetector.Data.AppMetaData;
using Serilog;
using ScanDetector.Infrastructure.Abstracts.Base;

namespace ScanDetector.Service.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public EmailService(IOptions<MailSettings> mailSettings, IUnitOfWork unitOfWork,ICurrentUserService currentUserService)
        {
            _mailSettings = mailSettings.Value;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }
        private async Task<object> getSettingValue(Guid UserId, string appSettingName)
        {
            var item = await _unitOfWork.AppSetting.GetByUserIdAndName(UserId, appSettingName);
            var setting = item.ToUserSettings();
            return setting.Value;
        }

        public async Task SendEmailFromSystemAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null)
        {

            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject = subject
            };

            email.To.Add(MailboxAddress.Parse(mailTo));

            var builder = new BodyBuilder();

            if (attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();

                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }


        public async Task<string> SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null)
        {
            try
            {
                var currentUserId = _currentUserService.GetUserId();
                var settings = new MailSettings
                {
                    Host = (string)await getSettingValue(currentUserId, AppSettingData.SmtpHost.Name),
                    Port = (int)await getSettingValue(currentUserId, AppSettingData.SmtpPort.Name),
                    UseSSL = (bool)await getSettingValue(currentUserId, AppSettingData.SmtpUseSSL.Name),
                    DisplayName = (string)await getSettingValue(currentUserId, AppSettingData.SmtpDisplayName.Name),
                    Email = (string)await getSettingValue(currentUserId, AppSettingData.SmtpEmail.Name),
                    Password = (string)await getSettingValue(currentUserId, AppSettingData.SmtpPassword.Name),
                };
                var email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(settings.Email),
                    Subject = subject
                };

                email.To.Add(MailboxAddress.Parse(mailTo));

                var builder = new BodyBuilder();

                if (attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in attachments)
                    {
                        if (file.Length > 0)
                        {
                            using var ms = new MemoryStream();
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();

                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }

                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();
                email.From.Add(new MailboxAddress(settings.DisplayName, settings.Email));

                using var client = new SmtpClient();
                client.ServerCertificateValidationCallback = (mysender, certificate, chain, sslPolicyErrors) => { return true; };
                client.CheckCertificateRevocation = false;

                client.SslProtocols =
                    //System.Security.Authentication.SslProtocols.Ssl2 |
                    //System.Security.Authentication.SslProtocols.Ssl3 |
                    System.Security.Authentication.SslProtocols.Tls |
                    System.Security.Authentication.SslProtocols.Tls11 |
                    System.Security.Authentication.SslProtocols.Tls12 |
                    System.Security.Authentication.SslProtocols.Tls13;

                var options = SecureSocketOptions.None;
                if (settings.UseSSL.HasValue && settings.UseSSL.Value)
                {
                    if (settings.Port == 465)
                        options = SecureSocketOptions.SslOnConnect;
                    else
                        options = SecureSocketOptions.StartTls;
                }
                await client.ConnectAsync(
                    settings.Host,
                    settings.Port,
                    //settings.SmtpUseSSL
                    options
                    );



                // Note: only needed if the SMTP server requires authentication
                if (!string.IsNullOrEmpty(settings.Email) && !string.IsNullOrEmpty(settings.Password))
                {
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");

                    await client.AuthenticateAsync(settings.Email, settings.Password);
                    //await client.AuthenticateAsync(new System.Net.NetworkCredential(settings.SmtpUser, settings.SmtpPassword));
                }


                var res = await client.SendAsync(email);
                await client.DisconnectAsync(true);

                //end of sending email
                return "Success";
            }
            catch (Exception ex)
            {
                Log.Error($"Unhandled exception : : {ex} {ex.Message}");
                return "Failed";
            }

        }
    }
}
