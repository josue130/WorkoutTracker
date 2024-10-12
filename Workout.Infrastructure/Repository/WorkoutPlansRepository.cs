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
    public class WorkoutPlansRepository : Repository<WorkoutPlan>, IWorkoutPlansRepository
    {
        private readonly AppDbContext _db;
        public WorkoutPlansRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public Task<List<WorkoutPlan>> GenerateReport(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public void Update(WorkoutPlan model)
        {
            _db.workoutPlans.Update(model);
        }
    }
}
