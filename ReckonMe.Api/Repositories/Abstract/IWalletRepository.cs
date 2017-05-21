using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ReckonMe.Api.Models;

namespace ReckonMe.Api.Repositories.Abstract
{
    public interface IWalletRepository
    {
        Task<IEnumerable<Wallet>> GetWalletsForUserAsync(string username);
        Task<Wallet> GetWalletAsync(ObjectId id);
        Task CreateWalletAsync(Wallet wallet);
        Task UpdateWalletAsync(Wallet wallet);
        Task DeleteWalletAsync(ObjectId id);
    }
}