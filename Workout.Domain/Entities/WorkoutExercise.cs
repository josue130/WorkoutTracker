using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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

        public WorkoutExercise(Guid id, int exerciseId, Guid workoutId, int sets, int repetitions, double weight)
        {
            Id = id;
            ExerciseId = exerciseId;
            WorkoutId = workoutId;
            Sets = sets;
            Repetitions = repetitions;
            Weight = weight;
        }

        private WorkoutExercise()
        {

        }

        public static WorkoutExercise Create(int exerciseId, Guid workoutId, int sets, int repetitions, double weight)
        {
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

        public static WorkoutExercise Update(Guid id,int exerciseId, Guid workoutId, int sets, int repetitions, double weight)
        {
            return new WorkoutExercise
            {
                Id = id,
                ExerciseId = exerciseId,
                WorkoutId = workoutId,
                Sets = sets,
                Repetitions = repetitions,
                Weight = weight
            };
        }

    }
}
