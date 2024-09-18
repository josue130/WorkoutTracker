using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApi.Models
{
    public class ScheduleWorkout
    {
        public int Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public virtual Workout Workout { get; set; } = null!;

        
    }
}
