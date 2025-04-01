using System.Reflection;

using Microsoft.AspNetCore.RateLimiting;
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

        public static void ConfigureRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                // Fixed Window Limiter (Allows 3 requests per 10 seconds)
                options.AddFixedWindowLimiter("Fixed", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(10);
                    opt.PermitLimit = 3;
                });

                // Sliding Window Limiter (Allows 10 requests per minute)
                options.AddSlidingWindowLimiter("SlidingWindow", opt =>
                {
                    opt.PermitLimit = 10;
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.SegmentsPerWindow = 4; // Each segment represents 15 seconds
                });

                options.RejectionStatusCode = 429;
            });
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