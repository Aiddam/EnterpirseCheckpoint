using EnterpriseCheckpoint.Models.Enum;

namespace EnterpriseCheckpoint.Models.Models
{
    public class User : BaseEntity
    {
        public string Login { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        public Employee Employee { get; set; } = null!; 
    }
}
