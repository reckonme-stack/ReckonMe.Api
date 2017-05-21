using System.Collections.Generic;
using System.Threading.Tasks;
using ReckonMe.Api.Dtos;

namespace ReckonMe.Api.Services.Abstract
{
    public interface IWalletService
    {
        Task<IEnumerable<WalletDto>> GetWalletsForUserAsync(string username);
        Task<WalletDto> GetWalletAsync(string id);
        Task AddWallet(AddWalletDto wallet);
        Task UpdateWallet(string id, EditWalletDto wallet);
        Task RemoveWallet(string id);
    }
}