namespace EnterpriseCheckpoint.Models.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int? DayOfWeekStart { get; set; }
        public int? DayOfWeekEnd { get; set; }
    }
}
