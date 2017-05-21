using System;
using FluentAssertions;
using NSubstitute;
using ReckonMe.Api.Cryptography.Concrete;
using Xunit;

namespace ReckonMe.Api.Tests.Cryptography
{
    public class PasswordHasherTests
    {
        private const string Password = "password";
        private readonly string _hashedPassword;

        public PasswordHasherTests()
        {
            var passwordHasher = new PasswordHasher();
            _hashedPassword = passwordHasher.HashPassword(Password);
        }

        [Fact]
        public void HashPassword_ShouldThrowException_WhenPasswordArgumentIsNull()
        {
            // arrange

            // act
            var passwordHasher = new PasswordHasher();
            Action result = () => passwordHasher.HashPassword(null);

            // assert
            result.ShouldThrow<ArgumentNullException>()
                .WithMessage("*password");
        }

        [Fact]
        public void VerifyHashedPassword_ShouldReturnFalse_WhenBothArgumentsAreNull()
        {
            // arrange

            // act
            var passwordHasher = new PasswordHasher();
            var result = passwordHasher.VerifyHashedPassword(null, null);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void VerifyHashedPassword_ShouldReturnFalse_WhenHashedPasswordArgumentIsNull()
        {
            // arrange

            // act
            var passwordHasher = new PasswordHasher();
            var result = passwordHasher.VerifyHashedPassword(null, Password);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public void VerifyHashedPassword_ShouldThrowException_WhenPasswordArgumentIsNull()
        {
            // arrange

            // act
            var passwordHasher = new PasswordHasher();
            Action result = () => passwordHasher.VerifyHashedPassword(_hashedPassword, null);

            // assert
            result.ShouldThrow<ArgumentNullException>()
                .WithMessage("*password");
        }

        [Fact]
        public void VerifyHashedPassword_ShouldReturnTrue_WhenPasswordIsOk()
        {
            // arrange

            // act 
            var passwordHasher = new PasswordHasher();
            var result = passwordHasher.VerifyHashedPassword(_hashedPassword, Password);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public void VerifyHashedPassword_ShouldReturnFalse_WhenPasswordIsIncorrect()
        {
            // arrange
            var incorrectPassword = "incorrectpassword";

            // act 
            var passwordHasher = new PasswordHasher();
            var result = passwordHasher.VerifyHashedPassword(_hashedPassword, incorrectPassword);

            // assert
            result.Should().BeFalse();
        }
    }
}