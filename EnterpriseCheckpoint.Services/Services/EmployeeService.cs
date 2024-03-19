using AutoMapper;
using EnterpirseCheckpoint.Utilities;
using EnterpirseCheckpoint.Utilities.Exceptions;
using Enterprise.Checkpoint.Interfaces.DataAccessInterfaces;
using Enterprise.Checkpoint.Interfaces.Services;
using Enterprise.Checkpoint.Interfaces.Utilities;
using EnterpriseCheckpoint.Models.Models;

namespace EnterpriseCheckpoint.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            var employeeRepository = await _unitOfWork.GetRepository<Employee>();

            await employeeRepository.CreateAsync(employee);

            await _unitOfWork.CommitAsync();
        }
    }
}
