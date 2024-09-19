using WorkoutApi.Data;
using WorkoutApi.Models;
using WorkoutApi.Repository.IRepository;

namespace WorkoutApi.Repository
{
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        private readonly AppDbContext _db;
        public ExerciseRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Exercise model)
        {
            _db.exercises.Update(model);
        }
    }
}
