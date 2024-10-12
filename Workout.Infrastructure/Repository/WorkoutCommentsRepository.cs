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
    public class WorkoutCommentsRepository : Repository<WorkoutComments>, IWorkoutCommentsRepository
    {
        private readonly AppDbContext _db;
        public WorkoutCommentsRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<WorkoutComments>> GetWorkoutComments(Guid workoutId)
        {
            return await _db.workoutComments.Where(workout => workout.WorkoutId == workoutId).ToListAsync();
        }

        public void Update(WorkoutComments model)
        {
            _db.workoutComments.Update(model);
        }
    }
}
