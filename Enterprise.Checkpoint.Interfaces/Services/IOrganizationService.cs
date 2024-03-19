
using EnterpriseCheckpoint.Models.Models;

namespace Enterprise.Checkpoint.Interfaces.Services
{
    public interface IOrganizationService
    {
        Task CreateAsync(Organization organization, CancellationToken cancellationToken = default);
        Task<Organization> GetOrganizationByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Organization> GetOrganizationByUserAsync(User user, CancellationToken cancellationToken = default);
    }
}
