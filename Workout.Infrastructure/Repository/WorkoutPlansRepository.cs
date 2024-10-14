
using Workout.Domain.Entities;
using Workout.Infrastructure.Data;
using Workout.Application.Common.Interfaces;
using Workout.Application.Common.Dto;
using Microsoft.EntityFrameworkCore;
namespace Workout.Infrastructure.Repository
{
    public class WorkoutPlansRepository : Repository<WorkoutPlan>, IWorkoutPlansRepository
    {
        private readonly AppDbContext _db;
        public WorkoutPlansRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<WorkoutPlanResponseDto>> GenerateReport(Guid userId)
        {
            IEnumerable<WorkoutPlanResponseDto> data = await _db.scheduleWorkouts
                     .AsNoTrackingWithIdentityResolution()
                     .Where(sw => sw.ScheduledDate < DateTime.Now && sw.Workout.UserId == userId)
                     .OrderByDescending(sw => sw.ScheduledDate)
                     .Select(sw => new WorkoutPlanResponseDto
                     {
                         Id = sw.WorkoutId,
                         Name = sw.Workout.Name,
                         Description = sw.Workout.Description,
                         ScheduledDate = sw.ScheduledDate

                     })
                     .ToListAsync();
            return data;
        }

        public async Task<IEnumerable<WorkoutPlanResponseDto>> GetAllWorkoutsByUserId(Guid userId)
        {
            IEnumerable<WorkoutPlanResponseDto> data = await _db.scheduleWorkouts
                     .AsNoTrackingWithIdentityResolution()
                     .Where(sw => sw.Workout.UserId == userId)
                     .OrderByDescending(sw => sw.ScheduledDate)
                     .Select(sw => new WorkoutPlanResponseDto
                     {
                         Id = sw.WorkoutId,
                         Name = sw.Workout.Name,
                         Description = sw.Workout.Description,
                         ScheduledDate = sw.ScheduledDate

                     })
                     .ToListAsync();
            return data;
        }
        
        public void Update(WorkoutPlan model)
        {
            _db.workoutPlans.Update(model);
        }

    }
}
