using Microsoft.EntityFrameworkCore;
using Workout.Domain.Entities;

namespace Workout.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Exercise> exercises { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<WorkoutPlan> workoutPlans { get; set; }
        public DbSet<WorkoutExercise> workoutExercises { get; set; }
        public DbSet<WorkoutComments> workoutComments { get; set; }
        public DbSet<ScheduleWorkout> scheduleWorkouts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise { Id = 1, Name = "Push-up", Description = "An exercise that targets the chest, shoulders, and triceps.", Category = "Strength" },
                new Exercise { Id = 2, Name = "Squat", Description = "A lower body exercise that targets the thighs and glutes.", Category = "Strength" },
                new Exercise { Id = 3, Name = "Pull-up", Description = "An upper body exercise that works the back and biceps.", Category = "Strength" },
                new Exercise { Id = 4, Name = "Plank", Description = "A core exercise that targets the abdominals and lower back.", Category = "Core" },
                new Exercise { Id = 5, Name = "Lunge", Description = "A lower body exercise that works the legs and glutes.", Category = "Strength" },
                new Exercise { Id = 6, Name = "Bicep Curl", Description = "An exercise that focuses on the biceps using weights.", Category = "Strength" },
                new Exercise { Id = 7, Name = "Deadlift", Description = "A strength exercise that targets the entire body, especially the back and legs.", Category = "Strength" },
                new Exercise { Id = 8, Name = "Bench Press", Description = "A chest exercise performed with a barbell or dumbbells.", Category = "Strength" },
                new Exercise { Id = 9, Name = "Crunch", Description = "An abdominal exercise that targets the upper abs.", Category = "Core" },
                new Exercise { Id = 10, Name = "Burpee", Description = "A full-body exercise that combines a squat, push-up, and jump.", Category = "Cardio" }
           );
        }
    }
}
