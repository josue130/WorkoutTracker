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
    public class WorkoutCommentsTests
    {
        [Fact]
        public void HandleCreateWorkouCommets_WhenAllTheValuesAreCorrect_ShouldReturnWorkoutCommentsWithCorrectValues()
        {
            // Arrange
            Guid workoutId = Guid.NewGuid();
            string comment = "Test comment";

            // Act
            var workoutComments = WorkoutComments.Create(workoutId,comment);

            // Assert
            workoutComments.Should().NotBeNull();
            workoutComments.Id.Should().NotBeEmpty();
            workoutComments.Comment.Should().Be(comment);
        }
        [Fact]
        public void HandleCreateWorkoutComment_WithInvalidComment_ShouldThrowArgumenException()
        {
            // Arrange
            Guid workoutId = Guid.NewGuid();
            string comment = " ";

            // Act
            Action act = () => WorkoutComments.Create(workoutId, comment);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage(ErrorMessages.CommentsCannotBeEmpty);
        }
    }
}
