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
    public class WorkoutExercisesRepository : Repository<WorkoutExercise>, IWorkoutExercisesRepository
    {
        private readonly AppDbContext _db;
        public WorkoutExercisesRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<WorkoutExercise>> GetWorkoutExercises(Guid workoutId)
        {
            return await _db.workoutExercises.Where(workout => workout.WorkoutId == workoutId).ToListAsync();
        }

        public void Update(WorkoutExercise model)
        {
            _db.workoutExercises.Update(model);
        }
    }
}
