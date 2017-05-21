using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReckonMe.Api.Dtos;
using ReckonMe.Api.FIlters;
using ReckonMe.Api.Services.Abstract;

namespace ReckonMe.Api.Controllers
{
    [Route("api/wallets")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForUser()
        {
            var wallets = await _walletService.GetWalletsForUserAsync(Username)
                .ConfigureAwait(false);
            return Ok(wallets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var wallet = await _walletService.GetWalletAsync(id)
                .ConfigureAwait(false);
            return Ok(wallet);
        }

        [HttpPost("add")]
        [Validate]
        public async Task<IActionResult> Add([FromBody]AddWalletDto walletDto)
        {
            await _walletService.AddWallet(walletDto)
                .ConfigureAwait(false);
            return NoContent();
        }

        [HttpPut("edit/{id}")]
        [Validate]
        public async Task<IActionResult> Edit(string id, [FromBody]EditWalletDto walletDto)
        {
            await _walletService.UpdateWallet(id, walletDto)
                .ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            await _walletService.RemoveWallet(id)
                .ConfigureAwait(false);
            return NoContent();
        }
    }
}