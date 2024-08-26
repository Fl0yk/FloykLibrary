using FloykLibrary.Application.Shared.Abstractions;
using FloykLibrary.Presentation.Options.Setups;
using FloykLibrary.Presentation.Services;

namespace FloykLibrary.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.ConfigureOptions();

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IImageService, ImageService>();

            return services;
        }

        private static IServiceCollection ConfigureOptions(this IServiceCollection services)
        {
            // KEEP launchSettings.json and applicatoinSettings.json in sync
            services.ConfigureOptions<WWWRootOptionsSetup>();

            return services;
        }
    }
}
