using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Models;

namespace OrderingService.Data.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                Database.Migrate();
            else
                Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new EmployeeProfileConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new ServiceOrderConfiguration());
            builder.ApplyConfiguration(new ServiceTypeConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());

            // Initialization of default roles
            var roles = new Role[]
            {
                new Role{Id = 1, Name = "user"},
                new Role{Id = 2, Name = "admin"}
            };
            builder.Entity<Role>().HasData(roles);

            // Initialization of data for content
            DataSeeder.Seed(builder);
        }
    }
}
