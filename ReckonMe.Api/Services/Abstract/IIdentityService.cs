using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using ReckonMe.Api.Dtos;

namespace ReckonMe.Api.Services.Abstract
{
    public interface IIdentityService
    {
        Task<bool> RegisterAsync(UserRegisterDto user);
        Task<string> LoginAsync(UserLoginDto user);
        TokenValidationParameters GenerateTokenValidationParameters();
    }
}