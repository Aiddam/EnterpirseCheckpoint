using EnterpriseCheckpoint.Models.Models;

namespace Enterprise.Checkpoint.Interfaces.Services
{
    public interface IEmployeeAssignmentService
    {
        Task<EmployeeDayAssignment> CreateEnterEmployeeAssignmentAsync(int employeeId, CancellationToken cancellationToken = default);
        Task<EmployeeDayAssignment> CreateExitEmployeeAssignmentAsync(int employeeId, CancellationToken cancellationToken = default);
        Task<bool> IsEnterAssignmentExistsAsync(int employeeId, CancellationToken cancellationToken = default);
        Task<bool> IsExitAssignmentExistsAsync(int employeeId, CancellationToken cancellationToken = default);
        Task<bool> IsWorkDayAsync(int employeeId, CancellationToken cancellationToken = default);
    }
}
