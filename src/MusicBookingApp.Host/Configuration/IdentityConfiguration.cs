using MusicBookingApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MusicBookingApp.Infrastructure.Data;

namespace MusicBookingApp.Host.Configuration;

public static class IdentityConfiguration
{
    /// <summary>
    /// Configure the Microsoft Identity framework
    /// </summary>
    /// <param name="services"></param>
    public static void SetupMsIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options =>
                {
                    // Password settings
                    // i made these weak for testing purposes.
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 3;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.AllowedForNewUsers = true;

                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                }).AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

        // generated tokens will only last 2 hours.
        services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromHours(2));
    }
}