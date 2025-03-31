using System.Reflection;

using Microsoft.Extensions.DependencyInjection.Extensions;

using MusicBookingApp.Application.Contracts;
using MusicBookingApp.Application.Repositories.Base;
using MusicBookingApp.Infrastructure.Repositories.Base;
using MusicBookingApp.Infrastructure.Services;

using NetCore.AutoRegisterDi;

namespace MusicBookingApp.Host.Configuration
{
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Register services in the DI container.
        /// </summary>
        /// <param name="services"></param>
        /// 

        public static IServiceCollection RegisterApplicationServices<T>(
               this IServiceCollection services
           )
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();

            var assemblyserviseToScan = Assembly.GetAssembly(typeof(T));
            services
                .RegisterAssemblyPublicNonGenericClasses(assemblyserviseToScan)
                .Where(x => x.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            return services;
        }

        public static IServiceCollection RegisterApplicationRepository<T>(
               this IServiceCollection services
           )
        {
            var assemblyserviseToScan = Assembly.GetAssembly(typeof(T));
            services
                .RegisterAssemblyPublicNonGenericClasses(assemblyserviseToScan)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsPublicImplementedInterfaces();

            return services;
        }
    }
}