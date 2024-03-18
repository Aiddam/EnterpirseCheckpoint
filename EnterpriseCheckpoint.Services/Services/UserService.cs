using EnterpirseCheckpoint.Utilities;
using EnterpirseCheckpoint.Utilities.Exceptions;
using Enterprise.Checkpoint.Interfaces.DataAccessInterfaces;
using Enterprise.Checkpoint.Interfaces.Services;
using Enterprise.Checkpoint.Interfaces.Utilities;
using EnterpriseCheckpoint.Models.Models;

namespace EnterpriseCheckpoint.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> LoginAsync(string login, string password, CancellationToken cancellationToken = default)
        {
            var userRepository = await _unitOfWork.GetRepository<User>();

            var user = await userRepository.ReadEntitiesByPredicate(u => u.Login == login, cancellationToken: cancellationToken);

            if (user == null || !user.Any())
            {
                throw new InvalidLoginException(login);
            }

            var foundUser = user.First();
            var passwordValidationResult = await _passwordHasher.VerifyPasswordHashAsync(password, foundUser.PasswordHash);

            if (!passwordValidationResult)
            {
                throw new InvalidPasswordException();
            }

            return foundUser;
        }
    }
}
