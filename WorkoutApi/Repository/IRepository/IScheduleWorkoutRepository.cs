using WorkoutApi.Models;

namespace WorkoutApi.Repository.IRepository
{
    public interface IScheduleWorkoutRepository : IRepository<ScheduleWorkout>
    {
        Task<IEnumerable<ScheduleWorkout>> GetScheduleWorkouts(Guid UserId);
        void Update(ScheduleWorkout model);
    }
}
