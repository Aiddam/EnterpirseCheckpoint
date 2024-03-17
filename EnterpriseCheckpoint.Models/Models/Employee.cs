namespace EnterpriseCheckpoint.Models.Models
{
    public class Employee : BaseEntity
    {
        public string Role { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; } = null!;
    }
}
