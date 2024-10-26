using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Domain.Constants;
using Workout.Domain.Entities;

namespace DomainUnitTests
{
    public class UserTests
    {
        [Fact]
        public void HandleCreateUser_WhenAllTheValuesAreCorrect_ShouldReturnUserWithCorrectValues()
        {
            // Arrange
            string fullName = "Pedrito LM";
            string email = "test@gmail.com";
            string userName = "Test";
            string password = "34432kjo432";

            // Act
            var user = User.Create(fullName, email, userName, password);

            // Assert

            user.Should().NotBeNull();
            user.Id.Should().NotBeEmpty();
            user.FullName.Should().Be(fullName);
            user.Email.Should().Be(email);  
            user.UserName.Should().Be(userName);
            user.Password.Should().Be(password);
        }

        [Fact]
        public void HandleCreateUser_WithInvalidFullName_ShooulRetunArgumentException()
        {
            // Arrange
            string fullName = " ";
            string email = "test@gmail.com";
            string userName = "Test";
            string password = "34432kjo432";

            // Act
            Action act = () => User.Create(fullName, email, userName, password);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage(ErrorMessages.FullNameCannotBeEmpty);
        }
        [Fact]
        public void HandleCreateUser_WithInvalidEmail_ShooulRetunArgumentException()
        {
            // Arrange
            string fullName = "Pedrito LM";
            string email = "testgmail.com";
            string userName = "Test";
            string password = "34432kjo432";

            // Act
            Action act = () => User.Create(fullName, email, userName, password);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage(ErrorMessages.InvalidEmailFormat);
        }
        [Fact]
        public void HandleCreateUser_WithInvalidUserName_ShooulRetunArgumentException()
        {
            // Arrange
            string fullName = "Pedrito LM";
            string email = "test@gmail.com";
            string userName = "te";
            string password = "34432kjo432";

            // Act
            Action act = () => User.Create(fullName, email, userName, password);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage(ErrorMessages.UserNameCharacters);
        }
    }
}
