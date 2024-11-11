using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Errors;
using Workout.Application.Services.Implementation;
using Workout.Domain.Entities;

namespace ApplicationUnitTests
{
    public class AuthServiceTests
    {
        
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _authService = new AuthService(_unitOfWorkMock.Object, _jwtTokenGeneratorMock.Object);
        }

        [Fact]
        public async Task Login_WithNonExistingUser_ShouldReturnFailureResult()
        {
            // Arrange
            var loginRequest = new LoginRequestDto { UserName = "nonexistentUser", Password = "password123" };
            _unitOfWorkMock.Setup(u => u.auth.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _authService.Login(loginRequest);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AuthError.UserNameNotExist);
        }


        [Fact]
        public async Task Login_WithIncorrectPassword_ShouldReturnFailureResult()
        {
            // Arrange
            var loginRequest = new LoginRequestDto { UserName = "existingUser", Password = "wrongPassword" };
            var user = new User(Guid.NewGuid(),"FullName", "user@example.com", "existingUser", "hashedPassword");
            _unitOfWorkMock.Setup(u => u.auth.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            var userDto = new UserDto { Name = user.FullName, UserName = user.UserName };
            var passwordHasher = new PasswordHasher<UserDto>();
            var hashedPassword = passwordHasher.HashPassword(userDto, "correctPassword");
            user.Password = hashedPassword;

            // Act
            var result = await _authService.Login(loginRequest);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AuthError.IncorrectPassword);
        }
        [Fact]
        public async Task Login_WithCorrectCredentials_ShouldReturnSuccessResult()
        {
            // Arrange
            var loginRequest = new LoginRequestDto { UserName = "existingUser", Password = "correctPassword" };
            var user = new User(Guid.NewGuid(), "FullName", "user@example.com", "existingUser", "hashedPassword"); ;
            _unitOfWorkMock.Setup(u => u.auth.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            var userDto = new UserDto { Name = user.FullName, UserName = user.UserName };
            var passwordHasher = new PasswordHasher<UserDto>();
            user.Password = passwordHasher.HashPassword(userDto, "correctPassword");

            var token = "mockedToken";
            _jwtTokenGeneratorMock.Setup(j => j.GenerateToken(user)).Returns(token);

            // Act
            var result = await _authService.Login(loginRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Register_WithInvalidEmail_ShouldReturnFailureResult()
        {
            // Arrange
            var request = new RegisterRequestDto
            {
                FullName = "New User",
                UserName = "newuser",
                Password = "password123",
                Email = "invalidEmailFormat"
            };

            // Act
            var result = await _authService.Register(request);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AuthError.InvalidEmailFormat);
        }

        [Fact]
        public async Task Register_WithValidData_ShouldReturnSuccessResult()
        {
            // Arrange
            var request = new RegisterRequestDto
            {
                FullName = "New User",
                UserName = "newuser",
                Password = "password123",
                Email = "newuser@example.com"
            };
            _unitOfWorkMock.Setup(u => u.auth.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _authService.Register(request);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Values.Should().Be("User registered successfully");
        }

        [Fact]
        public async Task Register_WithExistingUsername_ShouldReturnFailureResult()
        {
            // Arrange
            var request = new RegisterRequestDto
            {
                FullName = "New User",
                UserName = "existingUser",
                Password = "password123",
                Email = "newuser@example.com"
            };
            var existingUser = new User(Guid.NewGuid(), "FullName", "existing@example.com", "existingUser", "hashedPassword");
            _unitOfWorkMock.Setup(u => u.auth.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(existingUser);

            // Act
            var result = await _authService.Register(request);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(AuthError.UserNameAlreadyExits);
        }

    }
}
