using System.Reflection;
using ScanDetector.Infrastructure.Abstracts.Base;
using ScanDetector.Infrastructure.Data;
using ScanDetector.Infrastructure.Seeder;
using Microsoft.EntityFrameworkCore;
using Serilog;
using QuestPDF.Infrastructure;

namespace ScanDetector.Api
{
    public class Program
    {
        public static string EnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        private static Action<IConfigurationBuilder> BuildConfiguration =
                builder => builder
                    //.SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

        public static async Task Main(string[] args)
        {

            Log.Information($"Starting Server (Env : {EnvironmentName}) ...");

            var builder = new ConfigurationBuilder();
            BuildConfiguration(builder);


            Serilog.Log.Logger =
                    new Serilog.LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Build())
                    .CreateLogger();

            try
            {
                Log.Information("Creating host builder ...");
                var hostBuilder = CreateHostBuilder(args, builder);

                Log.Information("Building host ...");
                var host = hostBuilder.Build();
                QuestPDF.Settings.License = LicenseType.Community;

                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<ApplicationDbContext>();
                    //var en = dbContext.Database.EnsureCreated();
                    dbContext.Database.Migrate();
                    var unitOfWork = services.GetRequiredService<IUnitOfWork>();
                    await RoleSeeder.SeedAsync(unitOfWork);
                    await PermissionSeeder.SeedModuleAsync(unitOfWork);
                    await PermissionSeeder.SeedPermissionAsync(unitOfWork);
                    await UserSeeder.SeedAsync(unitOfWork);
                }
                catch (Exception ex)
                {
                    Log.Error($"Unhandled exception : {ex}");

                }
                Log.Information("Running host ...");
                host.Run();

            }
            catch (Exception ex)
            {
                Log.Error($"Unhandled exception : {ex}");
                Serilog.Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                Serilog.Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfigurationBuilder configurationBuilder)
        {
            var host = Host
                .CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureKestrel(serverOptions =>
                        {
                        })
                        .UseConfiguration(
                            configurationBuilder
                            .AddJsonFile("hosting.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"hosting.{EnvironmentName}.json", optional: true)
                            .Build()
                        )
                        .UseStartup<Startup>();
                });

            return host;
        }

    }

}

