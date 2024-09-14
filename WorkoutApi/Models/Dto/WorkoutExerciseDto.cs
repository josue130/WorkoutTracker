

using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutApi.Models.Dto
{
    public class WorkoutExerciseDto
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public double Weight { get; set; }
    }
}
