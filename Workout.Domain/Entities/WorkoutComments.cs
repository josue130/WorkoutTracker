using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workout.Domain.Entities
{
    public class WorkoutComments
    {
        public Guid Id { get; set; }
        public Guid WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public virtual WorkoutPlan Workout { get; set; } = null!;
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
