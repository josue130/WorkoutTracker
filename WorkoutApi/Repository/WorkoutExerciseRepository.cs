using Microsoft.EntityFrameworkCore;
using WorkoutApi.Data;
using WorkoutApi.Models;
using WorkoutApi.Repository.IRepository;

namespace WorkoutApi.Repository
{
    public class WorkoutExerciseRepository : Repository<WorkoutExercise>, IWorkoutExerciseRepository
    {
        private readonly AppDbContext _db;
        public WorkoutExerciseRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<WorkoutExercise>> GetWorkoutExercises(Guid workoutId)
        {
            return await _db.workoutExercises.Where(workout => workout.WorkoutId == workoutId).ToListAsync();
        }

        public void Update(WorkoutExercise model)
        {
            _db.workoutExercises.Update(model);
        }
    }
}
