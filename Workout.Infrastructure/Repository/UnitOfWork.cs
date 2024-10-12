
using Workout.Application.Common.Interfaces;
using Workout.Infrastructure.Data;

namespace Workout.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public IWorkoutPlansRepository workoutPlans { get; private set; }
        public IWorkoutCommentsRepository workoutsComments { get; private set; }
        public IWorkoutExercisesRepository workoutExercises { get; private set; }
        public IScheduleWorkoutRepository scheduleWorkouts { get; private set; }
        public IExerciseRepository exercises { get; private set; }
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            workoutPlans = new WorkoutPlansRepository(db);
            workoutsComments = new WorkoutCommentsRepository(db);
            workoutExercises = new WorkoutExercisesRepository(db);
            scheduleWorkouts = new ScheduleWorkoutRepository(db);
            exercises = new ExerciseRepository(db);
        }



        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
