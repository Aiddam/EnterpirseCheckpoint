using Enterprise.Checkpoint.Interfaces.DataAccessInterfaces;
using Enterprise.Checkpoint.Interfaces.Services;
using EnterpriseCheckpoint.Models.Models;

namespace EnterpriseCheckpoint.Services.Services
{
    public class EmployeeAssignmentService : IEmployeeAssignmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeAssignmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<EmployeeDayAssignment> CreateEnterEmployeeAssignmentAsync(int employeeId, CancellationToken cancellationToken = default)
            => CreateEmployeeAssignmentAsync(employeeId, true, cancellationToken);

        public Task<EmployeeDayAssignment> CreateExitEmployeeAssignmentAsync(int employeeId, CancellationToken cancellationToken = default)
            => CreateEmployeeAssignmentAsync(employeeId, false, cancellationToken);

        private async Task<EmployeeDayAssignment> CreateEmployeeAssignmentAsync(int employeeId, bool isEntered, CancellationToken cancellationToken = default)
        {
            var repository = await _unitOfWork.GetRepository<EmployeeDayAssignment>();

            var employeeDayAssignment = new EmployeeDayAssignment();

            employeeDayAssignment.AssignmentDate = DateTime.Now;
            employeeDayAssignment.EmployeeId = employeeId;
            employeeDayAssignment.IsEntered = isEntered;

            return await repository.CreateAsync(employeeDayAssignment, cancellationToken);
        }

        public Task<bool> IsEnterAssignmentExistsAsync(int employeeId, CancellationToken cancellationToken = default) 
            => IsAssignmentExistsAsync(employeeId, true, cancellationToken);

        public Task<bool> IsExitAssignmentExistsAsync(int employeeId, CancellationToken cancellationToken = default)
            => IsAssignmentExistsAsync(employeeId, true, cancellationToken);

        private async Task<bool> IsAssignmentExistsAsync(int employeeId, bool isEntered, CancellationToken cancellationToken)
        {
            var repository = await _unitOfWork.GetRepository<EmployeeDayAssignment>();

            var today = DateTime.Now;

            var employeeTodaysAssignments = await repository.ReadEntitiesByPredicate(eda => 
                eda.EmployeeId == employeeId &&
                eda.AssignmentDate > new DateTime(today.Year, today.Month, today.Day) && 
                eda.AssignmentDate < new DateTime(today.Year, today.Month, today.Day + 1)
            );

            return employeeTodaysAssignments.Any(eta => eta.IsEntered == isEntered);
        }

        public async Task<bool> IsWorkDayAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            var repository = await _unitOfWork.GetRepository<Employee>();
            var employee = await repository.ReadEntityByIdAsync(employeeId, cancellationToken);
            if (employee is null) return false;
            var today = DateTime.Now;
            return employee.DayOfWeekStart is not null &&
                employee.DayOfWeekEnd is not null &&
                employee.DayOfWeekStart <= (int)today.DayOfWeek &&
                employee.DayOfWeekEnd >= (int)today.DayOfWeek;
        }
    }
}
