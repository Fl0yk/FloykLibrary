using FloykLibrary.Application.Shared.Abstractions;
using FloykLibrary.Domain.Entities;
using FloykLibrary.Presentation.Constants;
using FloykLibrary.Presentation.Options.Models;
using FloykLibrary.Presentation.Options.Setups;
using FloykLibrary.Presentation.Providers;
using FloykLibrary.Presentation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

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
                    ValidIssuer = jwtOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),

                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyTypes.AdminPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(Role.Admin.ToString());
                });

                options.AddPolicy(PolicyTypes.ClientPolicy, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(Role.Client.ToString());
                });
            });
        }
    }
}
