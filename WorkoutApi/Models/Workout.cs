using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApi.Models
{
    public class Workout
    {
        public Guid Id { get; set; }
        [Required]
        public  string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
        
    }
}
