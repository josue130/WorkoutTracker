using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutApi.Models;

namespace WorkoutApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Exercise> exercises { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Workout> workouts { get; set; }
        public DbSet<WorkoutExercise> workoutExercises { get; set; }
        public DbSet<WorkoutComments> workoutComments { get; set; }
        public DbSet<ScheduleWorkout> scheduleWorkouts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
