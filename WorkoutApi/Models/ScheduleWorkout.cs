using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApi.Models
{
    public class ScheduleWorkout
    {
        public Guid Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public Guid WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public virtual Workout Workout { get; set; } = null!;

        
    }
}
