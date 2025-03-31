using Microsoft.OpenApi.Models;

namespace MusicBookingApp.Host.Configuration
{
    public static class SwaggerConfiguration
    {
        private const string SECURITY_SCHEME = "Bearer";

        public static void SetupSwagger(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // setup swagger to accept bearer tokens
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Music Booking API",
                        Description = "An API used to manage music bookings. Allows users to book events."
                    });

                options.AddSecurityDefinition(SECURITY_SCHEME, new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = SECURITY_SCHEME
                });
                options.AddSecurityRequirement(
   new OpenApiSecurityRequirement
   {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = SECURITY_SCHEME }
            },
            Array.Empty<string>()
        }
   }
);
            });

        }

        public static void RegisterSwagger(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                return;
            }

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}