using FloykLibrary.Application.Shared.Abstractions;
using FloykLibrary.Presentation.Options.Models;
using FloykLibrary.Presentation.Options.Setups;
using FloykLibrary.Presentation.Providers;
using FloykLibrary.Presentation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.Text;
using System.Text.Json;

namespace FloykLibrary.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions();
            
            services.ConfigureAuthorization(configuration);

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IJwtProvider, JwtProvider>();

            return services;
        }

        private static IServiceCollection ConfigureOptions(this IServiceCollection services)
        {
            // KEEP launchSettings.json and applicatoinSettings.json in sync
            services.ConfigureOptions<WWWRootOptionsSetup>();
            services.ConfigureOptions<JwtOptionsSetup>();

            return services;
        }

        private static void ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            JwtOptions jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>()
                                        ?? throw new KeyNotFoundException("Can't read jwt from appsettings.json");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),

                };
            });
        }
    }
}
