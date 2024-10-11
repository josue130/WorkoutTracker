using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workout.Domain.Entities
{
    public class WorkoutPlan
    {
        public Guid Id { get; set; }
        [Required]
        public  string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
        
    }
}
