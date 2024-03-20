using EnterpriseCheckpoint.Models.DTOs;
using EnterpriseCheckpoint.Models.Models;

namespace Enterprise.Checkpoint.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task CreateAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<IEnumerable<EmployeeDto>> GetOrganizationEmployeesAsync(int organizationId, CancellationToken cancellationToken = default);
        Task<Employee?> GetEmployeeByIdAsync(int employeeId, CancellationToken cancellationToken = default);
        Task<Employee> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
    }
}
