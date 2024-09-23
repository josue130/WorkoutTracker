using Microsoft.EntityFrameworkCore;
using WorkoutApi.Data;
using WorkoutApi.Models;
using WorkoutApi.Models.Dto;
using WorkoutApi.Repository.IRepository;

namespace WorkoutApi.Repository
{
    public class WorkoutRepository : Repository<Workout>, IWorkoutRepository
    {
        private readonly AppDbContext _db;  
        public WorkoutRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<ReportHeaderDto>> GenerateReport(Guid UserId)
        {
            List<ReportHeaderDto> report = await _db.scheduleWorkouts
                    .Where(sw => sw.ScheduledDate < DateTime.Now && sw.Workout.UserId == UserId)
                    .OrderBy(sw => sw.ScheduledDate)
                    .Select(sw => new ReportHeaderDto
                    {
                        WorkoutId = sw.Workout.Id,
                        WorkoutName = sw.Workout.Name,
                        ScheduledDate = sw.ScheduledDate,
                        ReportDetails = sw.Workout.WorkoutExercises.Select(we => new ReportDetailsDto
                        {
                            ExerciseName = we.Exercise.Name,
                            Sets = we.Sets,
                            Repetitions = we.Repetitions,
                            Weight = we.Weight
                        }).ToList()
                    }).ToListAsync();
 
            return report;
        }

        public void Update(Workout model)
        {
            _db.workouts.Update(model);
        }
    }
}
