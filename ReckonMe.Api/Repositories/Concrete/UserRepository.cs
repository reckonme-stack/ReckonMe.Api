using System.Threading.Tasks;
using MongoDB.Driver;
using ReckonMe.Api.Models;
using ReckonMe.Api.Repositories.Abstract;

namespace ReckonMe.Api.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<ApplicationUser> _usersCollection;

        public UserRepository(IMongoDatabase mongoDatabase)
        {
            _usersCollection = mongoDatabase.GetCollection<ApplicationUser>("users");
        }

        public async Task<ApplicationUser> GetUserAsync(string username)
            => await _usersCollection.Find(u => u.Username == username)
                .FirstOrDefaultAsync().ConfigureAwait(false);

        public async Task CreateUserAsync(ApplicationUser user)
            => await _usersCollection.InsertOneAsync(user)
                .ConfigureAwait(false);
    }
}