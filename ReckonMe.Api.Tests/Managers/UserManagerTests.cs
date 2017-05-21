using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ReckonMe.Api.Cryptography.Abstract;
using ReckonMe.Api.Managers.Concrete;
using ReckonMe.Api.Models;
using ReckonMe.Api.Repositories.Abstract;
using Xunit;

namespace ReckonMe.Api.Tests.Managers
{
    public class UserManagerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private const string Username = "username";
        private const string Password = "password";

        public UserManagerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnFalse_WhenGivenUserExistsInDatabase()
        {
            // arrange
            var user = new ApplicationUser {Username = Username};
            _userRepository.GetUserAsync(Username)
                .Returns(user);

            //act
            var userManager = new UserManager(_userRepository, _passwordHasher);
            var result = await userManager.CreateAsync(user);

            //assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnTrue_WhenGivenUserDoesNotExistInDatabase()
        {
            // arrange
            var user = new ApplicationUser { Username = Username };
            _userRepository.GetUserAsync(Arg.Any<string>())
                .ReturnsNullForAnyArgs();

            // act
            var userManager = new UserManager(_userRepository, _passwordHasher);
            var result = await userManager.CreateAsync(user);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task FindAsync_ShouldReturnNull_WhenGivenUserDoesNotExistInDatabase()
        {
            // arrange           
            _userRepository.GetUserAsync(Arg.Any<string>())
                .ReturnsNullForAnyArgs();

            // act
            var userManager = new UserManager(_userRepository, _passwordHasher);
            var result = await userManager.FindAsync(Username, Password);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task FindAsync_ShouldReturnNull_WhenGivenCredentialsAreNotValid()
        {
            // arrange
            var user = new ApplicationUser {Username = Username, Password = Password};
            _userRepository.GetUserAsync(Username)
                .Returns(user);
            _passwordHasher.VerifyHashedPassword(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsForAnyArgs(false);

            // act
            var userManager = new UserManager(_userRepository, _passwordHasher);
            var result = await userManager.FindAsync(Username, Password);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task FindAsync_ShouldReturnApplicationUser_WhenGivenCredentialsAreValid()
        {
            // arrange
            var user = new ApplicationUser { Username = Username, Password = Password };
            _userRepository.GetUserAsync(Username)
                .Returns(user);
            _passwordHasher.VerifyHashedPassword(Arg.Any<string>(), Arg.Any<string>())
                .ReturnsForAnyArgs(true);

            // act
            var userManager = new UserManager(_userRepository, _passwordHasher);
            var result = await userManager.FindAsync(Username, Password);

            // assert
            result.Should().Be(user);
        }
    }
}