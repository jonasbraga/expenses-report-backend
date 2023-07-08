using Microsoft.EntityFrameworkCore;

namespace AccountOrganizationService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Project> Projects => Set<Project>();
    }
}