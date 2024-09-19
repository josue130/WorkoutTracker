using WorkoutApi.Models;

namespace WorkoutApi.Repository.IRepository
{
    public interface IWorkoutCommentsRepository : IRepository<WorkoutComments>
    {
        Task<IEnumerable<WorkoutComments>> GetWorkoutComments(int workoutId);
        void Update(WorkoutComments model);
    }
}
