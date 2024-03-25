namespace EnterpriseCheckpoint.Models.Models
{
    public class Employee : BaseEntity
    {
        public string Role { get; set; } = string.Empty;
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int? DayOfWeekStart { get; set; }
        public int? DayOfWeekEnd { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; } = null!;
    }
}
