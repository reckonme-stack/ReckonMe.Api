using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ReckonMe.Api.Dtos;
using ReckonMe.Api.Managers.Abstract;
using ReckonMe.Api.Models;
using ReckonMe.Api.Options;
using ReckonMe.Api.Services.Abstract;

namespace ReckonMe.Api.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly JwtIssuerOptions _jwtOptions;

        public IdentityService(IOptions<JwtIssuerOptions> jwtOptions,
            IMapper mapper, IUserManager userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<bool> RegisterAsync(UserRegisterDto userDto)
        {
            var user = _mapper.Map<UserRegisterDto, ApplicationUser>(userDto);
            var result = await _userManager.CreateAsync(user).ConfigureAwait(false);
            return result;
        }

        public async Task<string> LoginAsync(UserLoginDto userDto)
        {
            var user = await _userManager.FindAsync(userDto.Username, userDto.Password).ConfigureAwait(false);
            if (user == null)
            {
                return null;
            }

            var claims = GenerateUserClaims(user.Email, user.Username);
            var jwt = GenerateSecurityToken(claims);
            return GenerateResponse(jwt);
        }

        public TokenValidationParameters GenerateTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = _jwtOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GenerateSecurityKey(),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
        }

        private static IEnumerable<Claim> GenerateUserClaims(string email, string username)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Email, email)
            };
        }

        private JwtSecurityToken GenerateSecurityToken(IEnumerable<Claim> claims)
        {
            return new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                GenerateClaims(claims),
                DateTime.UtcNow,
                DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtOptions.ExpirationTime)),
                GenerateSigningCredentials()
            );
        }

        private string GenerateResponse(JwtSecurityToken jwt)
        {
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var expires = (int)TimeSpan.FromMinutes(_jwtOptions.ExpirationTime).TotalSeconds;
            var response = new
            {
                access_token = encodedJwt,
                expires_in = expires
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        private static IEnumerable<Claim> GenerateClaims(IEnumerable<Claim> claims)
        {
            var defaultClaims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, GenerateJti()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    ToUnixEpochDate(DateTime.UtcNow).ToString(),
                    ClaimValueTypes.Integer64)
            };

            defaultClaims.AddRange(claims);

            return defaultClaims;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() -
                                 new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);

        private static string GenerateJti()
        {
            return Guid.NewGuid().ToString();
        }

        private SigningCredentials GenerateSigningCredentials()
        {
            return new SigningCredentials(GenerateSecurityKey(), SecurityAlgorithms.HmacSha256Signature);
        }

        private SymmetricSecurityKey GenerateSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret));
        }
    }
}