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
    public class ScheduleWorkoutTests
    {
        [Fact]
        public void HandleCreateSchedule_WhenAllTheValuesAreCorrect_ShouldReturnScheduleWorkoutWithCorrectValues()
        {
            // Arrange
            
            Guid workoutId = Guid.NewGuid();
            DateTime scheduledDate = DateTime.Now;

            // Act
            var scheduledWorkout = ScheduleWorkout.Create(scheduledDate, workoutId);

            // Assert
            scheduledWorkout.Should().NotBeNull();
            scheduledWorkout.Id.Should().NotBeEmpty();
            scheduledWorkout.WorkoutId.Should().Be(workoutId);
            scheduledWorkout.ScheduledDate.Should().Be(scheduledDate);
        }
        [Fact]
        public void HandleCreateScheduele_WithInvalidDate_ShouldThrowArgumenException()
        {
            // Arrange
            Guid workoutId = Guid.NewGuid();
            DateTime scheduledDate = DateTime.Now.AddDays(-10);

            // Act
            Action act = () => ScheduleWorkout.Create(scheduledDate, workoutId);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage(ErrorMessages.InvalidDate);
        }
    }
}
