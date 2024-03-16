using EnterpriseCheckpoint.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseCheckpoint.DataAccess.DbContexts
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
