using System.ComponentModel.DataAnnotations.Schema;

namespace Workout.Domain.Entities
{
    public class ScheduleWorkout
    {
        public Guid Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public Guid WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public virtual WorkoutPlan Workout { get; set; } = null!;

        
    }
}
