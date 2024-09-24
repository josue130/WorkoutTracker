using WorkoutApi.Models;

namespace WorkoutApi.Repository.IRepository
{
    public interface IWorkoutExerciseRepository : IRepository<WorkoutExercise>
    {
        Task<IEnumerable<WorkoutExercise>> GetWorkoutExercises(Guid workoutId);
        void Update(WorkoutExercise model);
    }
}
