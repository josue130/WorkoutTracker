using FluentAssertions;
using Workout.Domain.Constants;
using Workout.Domain.Entities;

namespace DomainUnitTests
{
    public class WorkoutExerciseTests
    {
        [Fact]
        public void HandleCreateWorkouExercise_WhenAllTheValuesAreCorrect_ShouldReturnWorkoutExerciseWithCorrectValues()
        {
            // Arrange
            int sets = 5;
            int repetitions = 6;
            double weight = 100;
            Guid workouId = Guid.NewGuid();

            // Act
            var workoutExersicePlan = WorkoutExercise.Create(2,workouId,sets,repetitions,weight);

            // Assert
            workoutExersicePlan.Should().NotBeNull();
            workoutExersicePlan.Id.Should().NotBeEmpty();
            workoutExersicePlan.ExerciseId.Should().Be(2);
            workoutExersicePlan.WorkoutId.Should().Be(workouId);
            workoutExersicePlan.Sets.Should().Be(sets);
            workoutExersicePlan.Repetitions.Should().Be(repetitions);
            workoutExersicePlan.Weight.Should().Be(weight);
        }

        [Fact]
        public void HandleCreateWorkoutExercise_WithInvalidSets_ShouldThrowArgumenException()
        {
            // Arrange
            int sets = -5;
            int repetitions = 6;
            double weight = 100;
            Guid workouId = Guid.NewGuid();

            // Act
            Action act = () => WorkoutExercise.Create(2, workouId, sets, repetitions, weight);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage(ErrorMessages.InvalidSets);
        }

        [Fact]
        public void HandleCreateWorkoutExercise_WithInvalidRepetitions_ShouldThrowArgumenException()
        {
            // Arrange
            int sets = 5;
            int repetitions = -6;
            double weight = 100;
            Guid workouId = Guid.NewGuid();

            // Act
            Action act = () => WorkoutExercise.Create(2, workouId, sets, repetitions, weight);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage(ErrorMessages.InvalidRepetitions);
        }

        [Fact]
        public void HandleCreateWorkoutExercise_WithInvalidWeight_ShouldThrowArgumenException()
        {
            // Arrange
            int sets = 5;
            int repetitions = 6;
            double weight = -100;
            Guid workouId = Guid.NewGuid();

            // Act
            Action act = () => WorkoutExercise.Create(2, workouId, sets, repetitions, weight);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage(ErrorMessages.InvalidWeight);
        }
    }
}
