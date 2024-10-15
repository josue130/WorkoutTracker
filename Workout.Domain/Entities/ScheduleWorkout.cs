using System.ComponentModel.DataAnnotations.Schema;
using Workout.Domain.ValueObjects;

namespace Workout.Domain.Entities
{
    public class ScheduleWorkout
    {
        public Guid Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public Guid WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public virtual WorkoutPlan Workout { get; set; } = null!;

        public static ScheduleWorkout Create(DateTime scheduledDate, Guid workoutId)
        {
            DateObject date = new DateObject(scheduledDate);
            return new ScheduleWorkout
            {
                Id= Guid.NewGuid(),
                ScheduledDate = date.Value,
                WorkoutId = workoutId
            };
        }
    }
}
