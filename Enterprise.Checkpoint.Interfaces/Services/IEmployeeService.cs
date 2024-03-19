
using EnterpriseCheckpoint.Models.Models;

namespace Enterprise.Checkpoint.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task CreateAsync(Employee employee, CancellationToken cancellationToken = default);
    }
}
