using Microsoft.EntityFrameworkCore;
using Workout.Domain.Entities;
using Workout.Infrastructure.Data;
using Workout.Application.Common.Interfaces;
namespace Workout.Infrastructure.Repository
{
    public class WorkoutExercisesRepository : Repository<WorkoutExercise>, IWorkoutExercisesRepository
    {
        private readonly AppDbContext _db;
        public WorkoutExercisesRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<WorkoutExercise>> GetWorkoutExercises(Guid workoutId, Guid userId)
        {
            return await _db.workoutExercises.Where(workout => workout.WorkoutId == workoutId && workout.Workout.UserId==userId).ToListAsync();
        }

        public void Update(WorkoutExercise model)
        {
            _db.workoutExercises.Update(model);
        }
    }
}
