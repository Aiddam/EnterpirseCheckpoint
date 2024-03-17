namespace EnterpriseCheckpoint.Models.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string HashKey { get; set; } = string.Empty;
    }
}
