using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using ReckonMe.Api.Dtos;
using ReckonMe.Api.Models;
using ReckonMe.Api.Repositories.Abstract;
using ReckonMe.Api.Services.Abstract;

namespace ReckonMe.Api.Services.Concrete
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;

        public WalletService(IWalletRepository walletRepository, IMapper mapper)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WalletDto>> GetWalletsForUserAsync(string username)
        {
            var wallets = await _walletRepository.GetWalletsForUserAsync(username)
                .ConfigureAwait(false);
            var result = _mapper.Map<IEnumerable<Wallet>, IEnumerable<WalletDto>>(wallets);

            return result;
        }

        public async Task<WalletDto> GetWalletAsync(string id)
        {
            var wallet = await _walletRepository.GetWalletAsync(ObjectId.Parse(id))
                .ConfigureAwait(false);
            var result = _mapper.Map<Wallet, WalletDto>(wallet);

            return result;
        }

        public async Task AddWallet(AddWalletDto wallet)
        {
            var result = _mapper.Map<AddWalletDto, Wallet>(wallet);
            await _walletRepository.CreateWalletAsync(result)
                .ConfigureAwait(false);
        }

        public async Task UpdateWallet(string id, EditWalletDto wallet)
        {
            var result = _mapper.Map<EditWalletDto, Wallet>(wallet);
            result.Id = ObjectId.Parse(id);
            await _walletRepository.UpdateWalletAsync(result)
                .ConfigureAwait(false);
        }

        public async Task RemoveWallet(string id)
            => await _walletRepository.DeleteWalletAsync(ObjectId.Parse(id))
                .ConfigureAwait(false);
    }
}