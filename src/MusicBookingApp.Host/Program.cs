using MusicBookingApp.Host.Configuration;
using MusicBookingApp.Infrastructure.Data;
using MusicBookingApp.Infrastructure.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var environment = builder.Environment.EnvironmentName;
        builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables()
            .Build();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowMyOrigin", x =>
            {
                x.WithOrigins("http://127.0.0.1:5500", "null", "http://localhost:3000")
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowCredentials();
            });
        });
        builder.Services.AddSignalR();

        builder.Services.SetupConfigFiles();
        builder.Services.SetupDatabase<DataContext>();
        builder.Services.SetupControllers();
        builder.Services.SetupSwagger();
        builder.Services.SetupFilters();
        builder.Services.SetupMsIdentity();
        builder.Services.SetupAuthentication();
        builder.Services.RegisterApplicationServices<AuthService>();
        builder.Services.SetupJsonOptions();
        builder.Services.AddFeatures();

        var app = builder.Build();
        await app.ApplyMigrations<DataContext>();
        app.UseCors("AllowMyOrigin");
        app.RegisterSwagger();
        app.RegisterMiddleware();
        app.Run();
    }
}

// for integration tests
namespace MusicBookingApp.Host
{
    public partial class Program;
}