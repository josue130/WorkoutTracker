namespace WorkoutApi.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IWorkoutRepository workouts { get; }
        IWorkoutCommentsRepository workoutsComments { get; }
        IWorkoutExerciseRepository workoutExercises { get; }
        IScheduleWorkoutRepository scheduleWorkouts { get; }
        IExerciseRepository exercises { get; }
        Task Save();
    }
}
