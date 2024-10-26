using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workout.Domain.Constants;

namespace Workout.Domain.Entities
{
    public class WorkoutExercise
    {
        public Guid Id { get; set; }
        public int ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public virtual Exercise Exercise { get; set; } = null!;
        public Guid WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public virtual WorkoutPlan Workout { get; set; } = null!;
        [Required]
        public int Sets { get; set; }
        [Required]
        public int Repetitions { get; set; }
        [Required]
        public double Weight { get; set; }

        public static WorkoutExercise Create(int exerciseId, Guid workoutId, int sets, int repetitions, double weight)
        {

            if (sets <= 0) throw new ArgumentException(ErrorMessages.InvalidSets);
            if (repetitions <= 0) throw new ArgumentException(ErrorMessages.InvalidRepetitions);
            if (weight < 0) throw new ArgumentException(ErrorMessages.InvalidWeight);

            return new WorkoutExercise 
            {
                Id = Guid.NewGuid(),
                ExerciseId = exerciseId,
                WorkoutId = workoutId,
                Sets = sets,
                Repetitions = repetitions,
                Weight = weight
            };
        }
    }
}
