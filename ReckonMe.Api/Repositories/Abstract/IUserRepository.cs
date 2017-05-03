using System.Threading.Tasks;
using ReckonMe.Api.Models;

namespace ReckonMe.Api.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserAsync(string username);
        Task AddUserAsync(ApplicationUser user);

    }
}