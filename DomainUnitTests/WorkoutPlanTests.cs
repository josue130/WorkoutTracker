using FluentAssertions;
using Workout.Domain.Entities;

namespace DomainUnitTests
{
    public class WorkoutPlanTests
    {
        [Fact]
        public void HandleCreateWorkoutPlan_WhenAllTheValuesAreCorrect_ShouldReturnWorkoutPlanWithCorrectValues()
        {
            // Arrange
            var name = "Full Body Workout";
            var description = "A workout plan targeting all major muscle groups.";
            var userId = Guid.NewGuid();

            // Act
            var workoutPlan = WorkoutPlan.Create(name, description, userId);

            // Assert
            workoutPlan.Should().NotBeNull();
            workoutPlan.Id.Should().NotBeEmpty();
            workoutPlan.Name.Should().Be(name);
            workoutPlan.Description.Should().Be(description);
            workoutPlan.UserId.Should().Be(userId);
            workoutPlan.WorkoutExercises.Should().BeEmpty();
        }

        [Fact]
        public void HandleCreateWorkoutPlan_WithInvalidName_ShouldThrowArgumenException()
        {
            // Arrange
            var invalidName = "";
            var description = "Valid Description";
            var userId = Guid.NewGuid();

            // Act
            Action act = () => WorkoutPlan.Create(invalidName, description, userId);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("The name cannot be empty.");
        }
        [Fact]
        public void HandleCreateWorkoutPlan_WithInvalidDescription_ShouldThrowArgumenException()
        {
            // Arrange
            var invalidName = "Full Body Workout";
            var description = "  ";
            var userId = Guid.NewGuid();

            // Act
            Action act = () => WorkoutPlan.Create(invalidName, description, userId);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("The description cannot be empty.");
        }


    }
}