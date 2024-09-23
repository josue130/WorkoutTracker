using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApi.Models
{
    public class WorkoutComments
    {
        public Guid Id { get; set; }
        public Guid WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public virtual Workout Workout { get; set; } = null!;
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
