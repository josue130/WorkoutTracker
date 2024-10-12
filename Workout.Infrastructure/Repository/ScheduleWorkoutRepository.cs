using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Interfaces;
using Workout.Domain.Entities;
using Workout.Infrastructure.Data;

namespace Workout.Infrastructure.Repository
{
    public class ScheduleWorkoutRepository : Repository<ScheduleWorkout>, IScheduleWorkoutRepository
    {
        private readonly AppDbContext _db;
        public ScheduleWorkoutRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ScheduleWorkout>> GetScheduleWorkouts(Guid UserId)
        {
            IEnumerable<ScheduleWorkout> data = await _db.scheduleWorkouts
                     .Include(sw => sw.Workout)
                     .Where(sw => sw.ScheduledDate >= DateTime.Now && sw.Workout.UserId == UserId)
                     .OrderBy(sw => sw.ScheduledDate)
                     .ToListAsync();
            return data;
        }

        public void Update(ScheduleWorkout model)
        {
            _db.scheduleWorkouts.Update(model);
        }
    }
}
