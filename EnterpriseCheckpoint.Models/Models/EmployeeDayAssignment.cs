namespace EnterpriseCheckpoint.Models.Models
{
    public class EmployeeDayAssignment : BaseEntity
    {
        public DateTime AssignmentDate { get; set; }
        public bool IsEntered { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
