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
        public void Update(WorkoutExercise model)
        {
            _db.workoutExercises.Update(model);
        }
    }
}
