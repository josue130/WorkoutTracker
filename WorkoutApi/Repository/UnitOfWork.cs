using WorkoutApi.Data;
using WorkoutApi.Repository.IRepository;

namespace WorkoutApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public IWorkoutRepository workouts { get; private set; }
        public IWorkoutCommentsRepository workoutsComments { get; private set; }
        public IWorkoutExerciseRepository workoutExercises { get; private set; }
        public IWorkoutScheduleRepository scheduleWorkouts { get; private set; }
        public IExerciseRepository exercises { get; private set; }
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            workouts = new WorkoutRepository(db);
            workoutsComments = new WorkoutCommentsRepository(db);
            workoutExercises = new WorkoutExerciseRepository(db);
            scheduleWorkouts = new WorkoutScheduleRepository(db);
            exercises = new ExerciseRepository(db);
        }

       

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
