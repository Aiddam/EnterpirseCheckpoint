using EnterpriseCheckpoint.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EnterpriseCheckpoint.DataAccess.DbContexts
{
    public class MainDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MainDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MainDbContext()
        {

        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SQLProviderConnectionString"));
            optionsBuilder.UseSqlServer("Server=ZHEKA;Database=TestDb;Trusted_Connection=True;Encrypt=False;");
        }
    }
}
