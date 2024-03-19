using EnterpriseCheckpoint.Models.Models;

namespace Enterprise.Checkpoint.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> LoginAsync(string login, string password, CancellationToken cancellationToken = default);
        Task RegistrationAsync(UserDto userDto, CancellationToken cancellationToken = default);
    }
}
