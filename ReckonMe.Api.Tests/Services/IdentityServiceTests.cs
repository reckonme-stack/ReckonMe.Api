using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ReckonMe.Api.Dtos;
using ReckonMe.Api.Managers.Abstract;
using ReckonMe.Api.Models;
using ReckonMe.Api.Options;
using ReckonMe.Api.Services.Concrete;
using Xunit;

namespace ReckonMe.Api.Tests.Services
{
    public class IdentityServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly IOptions<JwtIssuerOptions> _jwtOptions;
        private const string Username = "username";
        private const string Password = "password";
        private const string Email = "username@host.com";

        public IdentityServiceTests()
        {
            _mapper = Substitute.For<IMapper>();
            _userManager = Substitute.For<IUserManager>();
            _jwtOptions = Substitute.For<IOptions<JwtIssuerOptions>>();
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenGivenUserDoesNotExistInDatabase()
        {
            // arrange
            var userDto = new UserLoginDto {Username = Username, Password = Password};
            _userManager.FindAsync(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            // act
            var identityService = new IdentityService(_jwtOptions, _mapper, _userManager);
            var result = await identityService.LoginAsync(userDto);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenGivenUserHasValidCredentials()
        {
            // arrange
            var userDto = new UserLoginDto {Username = Username, Password = Password};
            var user = new ApplicationUser
            {
                Email = Email,
                Username = Username,
                Password = Password
            };
            _userManager.FindAsync(Username, Password).Returns(user);
            var options = new JwtIssuerOptions
            {
                Issuer = "issuer",
                Audience = "audience",
                Secret = "verystrongsecret",
                ExpirationTime = 5
            };
            _jwtOptions.Value.Returns(options);

            // act
            var identityService = new IdentityService(_jwtOptions, _mapper, _userManager);
            var result = await identityService.LoginAsync(userDto);

            // assert
            result.Should().BeOfType<string>();
        }
    }
}
