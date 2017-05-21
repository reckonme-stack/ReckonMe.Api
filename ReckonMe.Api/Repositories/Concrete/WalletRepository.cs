using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ReckonMe.Api.Models;
using ReckonMe.Api.Repositories.Abstract;

namespace ReckonMe.Api.Repositories.Concrete
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IMongoCollection<Wallet> _walletsCollection;

        public WalletRepository(IMongoDatabase mongoDatabase)
        {
            _walletsCollection = mongoDatabase.GetCollection<Wallet>("wallets");
        }

        public async Task<IEnumerable<Wallet>> GetWalletsForUserAsync(string username)
            => await _walletsCollection.Find(w => w.Owner == username).ToListAsync()
                .ConfigureAwait(false);

        public async Task<Wallet> GetWalletAsync(ObjectId id)
            => await _walletsCollection.Find(w => w.Id == id).SingleOrDefaultAsync()
                .ConfigureAwait(false);

        public async Task CreateWalletAsync(Wallet wallet)
            => await _walletsCollection.InsertOneAsync(wallet)
                .ConfigureAwait(false);

        public async Task UpdateWalletAsync(Wallet wallet)
            => await _walletsCollection.ReplaceOneAsync(w => w.Id == wallet.Id, wallet)
                .ConfigureAwait(false);

        public async Task DeleteWalletAsync(ObjectId id)
            => await _walletsCollection.DeleteOneAsync(w => w.Id == id)
                .ConfigureAwait(false);
    }
}