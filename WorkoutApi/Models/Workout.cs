using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApi.Models
{
    public class Workout
    {
        public int Id { get; set; }
        [Required]
        public  string Name { get; set; } = string.Empty;
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
        
    }
}
