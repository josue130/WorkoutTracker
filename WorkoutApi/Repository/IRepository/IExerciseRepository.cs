using WorkoutApi.Models;

namespace WorkoutApi.Repository.IRepository
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        void Update(Exercise model);
    }
}
