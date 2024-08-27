using FloykLibrary.Domain.Abstractions;
using FloykLibrary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FloykLibrary.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")
                                                        ?? throw new ArgumentNullException("Не найдена строка подключения к базе данных");

            services.AddDbContext<ApplicationDbContext>(cfg => cfg.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
