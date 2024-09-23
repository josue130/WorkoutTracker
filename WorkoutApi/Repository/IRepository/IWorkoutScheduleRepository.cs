using WorkoutApi.Models;

namespace WorkoutApi.Repository.IRepository
{
    public interface IWorkoutScheduleRepository : IRepository<ScheduleWorkout>
    {
        Task<IEnumerable<ScheduleWorkout>> GetScheduleWorkouts(Guid UserId);
        void Update(ScheduleWorkout model);
    }
}
