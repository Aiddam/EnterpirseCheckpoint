using AutoMapper;
using EnterpirseCheckpoint.Utilities.Exceptions;
using Enterprise.Checkpoint.Interfaces.DataAccessInterfaces;
using Enterprise.Checkpoint.Interfaces.Services;
using Enterprise.Checkpoint.Interfaces.Utilities;
using EnterpriseCheckpoint.Models.Models;

namespace EnterpriseCheckpoint.Services.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Organization organization, CancellationToken cancellationToken = default)
        {
            var organizationRepository = await _unitOfWork.GetRepository<Organization>();

            await organizationRepository.CreateAsync(organization);
        }

        public async Task<Organization> GetOrganizationByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var organizationRepository = await _unitOfWork.GetRepository<Organization>();

            var organization = await organizationRepository.ReadEntitiesByPredicate(o => o.Id == id);

            return organization.First();
        }

        public async Task<Organization> GetOrganizationByUserAsync(User user, CancellationToken cancellationToken = default)
        {
            var organizationRepository = await _unitOfWork.GetRepository<Organization>();

            var organization = await organizationRepository.ReadEntitiesByPredicate(o => o.UserId == user.Id);

            return organization.First();
        }
    }
}
