using System.Threading.Tasks;
using ReckonMe.Api.Models;

namespace ReckonMe.Api.Managers.Abstract
{
    public interface IUserManager
    {
        Task<bool> CreateAsync(ApplicationUser user);
        Task<ApplicationUser> FindAsync(string username, string password);
    }
}