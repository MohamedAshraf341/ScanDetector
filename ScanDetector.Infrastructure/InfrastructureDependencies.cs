using ScanDetector.Infrastructure.Abstracts.Base;
using ScanDetector.Infrastructure.Data;
using ScanDetector.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ScanDetector.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //dbContext.Database.EnsureCreated();
            //dbContext.Database.Migrate();
        }
    }
}
