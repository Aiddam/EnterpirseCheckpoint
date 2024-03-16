using EnterpriseCheckpoint.Models.Enum;

namespace EnterpriseCheckpoint.Models.Models
{
    public class Organization : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public OrganizationType OrganizationType { get; set; }
        public string TaxCode { get; set; } = string.Empty;
        public int PartnerId { get; set; }
        public Partner Partner { get; set; } = null!;
    }
}
