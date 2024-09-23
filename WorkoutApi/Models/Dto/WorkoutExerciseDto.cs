

using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApi.Models.Dto
{
    public class WorkoutExerciseDto
    {
        public Guid Id { get; set; }
        public Guid WorkoutId { get; set; }
        public int ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public double Weight { get; set; }
    }
}
