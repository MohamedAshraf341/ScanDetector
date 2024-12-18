using ScanDetector.Service.Abstracts;
using ScanDetector.Service.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace ScanDetector.Service
{
    public static class ServiceDependencies
    {
        public static void AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IPhotoCloudService, PhotoCloudService>();
            services.AddScoped<IFileService, FileService>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddScoped<PdfReportByDinkToPdf>();
            services.AddScoped<PdfReportByQuestPDF>();
            services.AddScoped<ExcelReportByClosedXML>();

        }
    }
}
