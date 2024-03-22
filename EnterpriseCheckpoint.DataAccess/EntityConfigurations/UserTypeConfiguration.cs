using EnterpriseCheckpoint.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseCheckpoint.DataAccess.EntityConfigurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
