using AutoMapper;
using Enterprise.Checkpoint.Interfaces.DataAccessInterfaces;
using Enterprise.Checkpoint.Interfaces.Services;
using EnterpriseCheckpoint.Models.DTOs;
using EnterpriseCheckpoint.Models.Models;

namespace EnterpriseCheckpoint.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            var employeeRepository = await _unitOfWork.GetRepository<Employee>();

            await employeeRepository.CreateAsync(employee);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> GetOrganizationEmployeesAsync(int organizationId, CancellationToken cancellationToken = default)
        {
            var employeeRepository = await _unitOfWork.GetRepository<Employee>();

            var organizationEmployees = await employeeRepository.ReadEntitiesByPredicate(e => e.OrganizationId == organizationId, cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<EmployeeDto>>(organizationEmployees);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            var employeeRepository = await _unitOfWork.GetRepository<Employee>();
            return await employeeRepository.ReadEntityByIdAsync(employeeId, cancellationToken);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            var employeeRepository = await _unitOfWork.GetRepository<Employee>();
            return await employeeRepository.UpdateAsync(employee, cancellationToken);
        }
    }
}
