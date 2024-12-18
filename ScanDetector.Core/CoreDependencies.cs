using AutoMapper;
using ScanDetector.Core.Behavior;
using ScanDetector.Core.Mapping.AppSetting;
using ScanDetector.Core.Mapping.Authentication;
using ScanDetector.Core.Mapping.Role;
using ScanDetector.Core.Mapping.User;
using ScanDetector.Core.Mapping.UserProfile;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ScanDetector.Core.Mapping.UsersRelative;
using ScanDetector.Core.Mapping.ScannerResult;

namespace ScanDetector.Core
{
    public static class CoreDependencies
    {
        public static void AddCoreDependencies(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RoleProfile>();
                cfg.AddProfile<AuthenticationProfile>();
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<UserProfileProfile>();
                cfg.AddProfile<AppSettingProfile>();
                cfg.AddProfile<UsersRelativeProfile>();
                cfg.AddProfile<ScannerResultProfile>();

            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
