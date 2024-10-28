using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Interfaces;
using Workout.Application.Services.Implementation;
using Workout.Domain.Entities;
using Workout.Domain.Exceptions;

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
        public async Task Login_ShouldSucceed_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequest = new LoginRequestDto { UserName = "testuser", Password = "password123" };
            var user = new User { UserName = "testuser", Password = "hashedpassword", FullName = "Test User" };

            _unitOfWorkMock.Setup(u => u.auth.Get(It.IsAny<Expression<Func<User, bool>>>()))
                           .ReturnsAsync(user);

            var passwordHasher = new PasswordHasher<UserDto>();
            var userDto = new UserDto { UserName = "testuser", Name = "Test User" };
            var hashResult = passwordHasher.HashPassword(userDto, "password123");
            _unitOfWorkMock.Setup(u => u.auth.Get(It.IsAny<Expression<Func<User, bool>>>()))
                           .ReturnsAsync(new User { UserName = "testuser", Password = hashResult });

            _jwtTokenGeneratorMock.Setup(j => j.GenerateToken(It.IsAny<User>())).Returns("mockToken");

            // Act
            var result = await _authService.Login(loginRequest);

            // Assert
            result.Token.Should().Be("mockToken");
            result.User.UserName.Should().Be("testuser");
        }
        [Fact]
        public async Task Login_ShouldThrowException_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginRequest = new LoginRequestDto { UserName = "wronguser", Password = "wrongpassword" };

            _unitOfWorkMock.Setup(u => u.auth.Get(It.IsAny<Expression<Func<User, bool>>>()))
                           .ReturnsAsync((User)null);

            // Act
            Func<Task> act = async () => await _authService.Login(loginRequest);

            // Assert
            await act.Should().ThrowAsync<UserNameOrPasswordIncorrectException>();
        }
        [Fact]
        public async Task Register_ShouldSucceed_WhenUserNameIsUnique()
        {
            // Arrange
            var registerRequest = new RegisterRequestDto
            {
                FullName = "New User",
                UserName = "newuser",
                Password = "password123",
                Email = "newuser@example.com"
            };

            _unitOfWorkMock.Setup(u => u.auth.Get(It.IsAny<Expression<Func<User, bool>>>()))
                           .ReturnsAsync((User)null);

            _unitOfWorkMock.Setup(u => u.auth.Add(It.IsAny<User>()));
            _unitOfWorkMock.Setup(u => u.Save()).Returns(Task.CompletedTask);

            // Act
            await _authService.Register(registerRequest);

            // Assert
            _unitOfWorkMock.Verify(u => u.auth.Add(It.IsAny<User>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }
        [Fact]
        public async Task Register_ShouldThrowException_WhenUserNameAlreadyExists()
        {
            // Arrange
            var registerRequest = new RegisterRequestDto
            {
                FullName = "Existing User",
                UserName = "existinguser",
                Password = "password123",
                Email = "existinguser@example.com"
            };

            _unitOfWorkMock.Setup(u => u.auth.Get(It.IsAny<Expression<Func<User, bool>>>()))
                           .ReturnsAsync(new User { UserName = "existinguser" });

            // Act
            Func<Task> act = async () => await _authService.Register(registerRequest);

            // Assert
            await act.Should().ThrowAsync<UserNameAlreadyExistsException>();
        }


    }
}
