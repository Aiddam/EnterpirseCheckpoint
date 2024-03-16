using EnterpriseCheckpoint.Models.Enum;

namespace EnterpriseCheckpoint.Models.Models
{
    public class Partner : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public PartnerType PartnerType { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Organization> Organizations { get; set; } = null!;
    }
}
