using Microsoft.EntityFrameworkCore;
using WorkoutApi.Data;
using WorkoutApi.Models;
using WorkoutApi.Repository.IRepository;

namespace WorkoutApi.Repository
{
    public class WorkoutScheduleRepository : Repository<ScheduleWorkout>, IWorkoutScheduleRepository
    {
        private readonly AppDbContext _db;
        public WorkoutScheduleRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ScheduleWorkout>> GetScheduleWorkouts(int UserId)
        {
            IEnumerable<ScheduleWorkout> data = await _db.scheduleWorkouts
                     .Include(sw => sw.Workout)
                     .Where(sw => sw.ScheduledDate >= DateTime.Now && sw.Workout.UserId == UserId)
                     .OrderBy(sw => sw.ScheduledDate)
                     .ToListAsync();
            return data;
        }

        public void Update(ScheduleWorkout model)
        {
            _db.scheduleWorkouts.Update(model);
        }
    }
}
