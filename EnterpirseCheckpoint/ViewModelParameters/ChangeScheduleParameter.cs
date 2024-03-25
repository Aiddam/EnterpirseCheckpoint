using EnterpriseCheckpoint.Models.Models;

namespace EnterpirseCheckpoint.ViewModelParameters
{
    public class ChangeScheduleParameter
    {
        public User CurrentUser { get; set; } = null!;
        public int EmployeeId { get; set; }
    }
}
