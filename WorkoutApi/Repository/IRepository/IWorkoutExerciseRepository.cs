using WorkoutApi.Models;

namespace WorkoutApi.Repository.IRepository
{
    public interface IWorkoutExerciseRepository : IRepository<WorkoutExercise>
    {
        void Update(WorkoutExercise model);
    }
}
