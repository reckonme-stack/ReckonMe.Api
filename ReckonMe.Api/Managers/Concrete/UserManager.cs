using System.Threading.Tasks;
using ReckonMe.Api.Cryptography.Abstract;
using ReckonMe.Api.Managers.Abstract;
using ReckonMe.Api.Models;
using ReckonMe.Api.Repositories.Abstract;

namespace ReckonMe.Api.Managers.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserManager(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> CreateAsync(ApplicationUser user)
        {
            var dbEntry = await _userRepository.GetUserAsync(user.Username).ConfigureAwait(false);
            if (dbEntry != null)
            {
                return false;
            }

            user.Password = _passwordHasher.HashPassword(user.Password);
            await _userRepository.AddUserAsync(user).ConfigureAwait(false);
            return true;
        }

        public async Task<ApplicationUser> FindAsync(string username, string password)
        {
            var user = await _userRepository.GetUserAsync(username).ConfigureAwait(false);
            if (user == null)
            {
                return null;
            }
            return _passwordHasher.VerifyHashedPassword(user.Password, password) ? user : null;
        }
    }
}