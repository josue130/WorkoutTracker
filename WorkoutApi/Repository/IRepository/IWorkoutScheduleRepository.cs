using WorkoutApi.Models;

namespace WorkoutApi.Repository.IRepository
{
    public interface IWorkoutScheduleRepository : IRepository<ScheduleWorkout>
    {
        Task<IEnumerable<ScheduleWorkout>> GetScheduleWorkouts(int UserId);
        void Update(ScheduleWorkout model);
    }
}
