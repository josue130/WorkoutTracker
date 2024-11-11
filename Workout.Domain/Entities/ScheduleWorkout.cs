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

        public ScheduleWorkout(Guid id, DateTime scheduledDate, Guid workoutId)
        {
            Id = id;
            ScheduledDate = scheduledDate;
            WorkoutId = workoutId;
        }

        private ScheduleWorkout()
        {
            
        }


        public static ScheduleWorkout Create(DateTime scheduledDate, Guid workoutId)
        {
            return new ScheduleWorkout
            {
                Id= Guid.NewGuid(),
                ScheduledDate = scheduledDate,
                WorkoutId = workoutId
            };
        }
        public static ScheduleWorkout Update(Guid id,DateTime scheduledDate, Guid workoutId)
        {
            return new ScheduleWorkout
            {
                Id = id,
                ScheduledDate = scheduledDate,
                WorkoutId = workoutId
            };
        }

    }
}
