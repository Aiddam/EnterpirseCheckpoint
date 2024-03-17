using EnterpriseCheckpoint.Models.Enum;

namespace EnterpriseCheckpoint.Models.Models
{
    public class Organization : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public OrganizationType OrganizationType { get; set; }
        public string TaxCode { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
