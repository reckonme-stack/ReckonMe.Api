using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReckonMe.Api.Dtos;
using ReckonMe.Api.FIlters;
using ReckonMe.Api.Services.Abstract;

namespace ReckonMe.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [Validate]
        public async Task<IActionResult> Login([FromBody]UserLoginDto userDto)
        {
            var token = await _identityService.LoginAsync(userDto)
                .ConfigureAwait(false);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [Validate]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto userDto)
        {
            var result = await _identityService.RegisterAsync(userDto)
                .ConfigureAwait(false);
            if (!result)
            {
                return StatusCode(409, "User with this username already exists!");
            }

            return NoContent();
        }
    }
}