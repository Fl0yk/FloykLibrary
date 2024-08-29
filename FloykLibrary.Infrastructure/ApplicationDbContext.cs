using FloykLibrary.Domain.Entities;
using FloykLibrary.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace FloykLibrary.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();

        public DbSet<Author> Authors => Set<Author>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Role> Roles => Set<Role>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) 
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfigurator());
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfigurator());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfigurator());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfigurator());

            base.OnModelCreating(modelBuilder);
        }
    }
}
