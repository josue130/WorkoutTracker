using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WorkoutApi.Data;
using WorkoutApi.Models;
using WorkoutApi.Repository.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkoutApi.Repository
{
    public class WorkoutCommentsRepository : Repository<WorkoutComments>, IWorkoutCommentsRepository
    {
        private readonly AppDbContext _db;
        public WorkoutCommentsRepository(AppDbContext db) :base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<WorkoutComments>> GetWorkoutComments(int workoutId)
        {
            return await _db.workoutComments.Where(workout => workout.WorkoutId == workoutId).ToListAsync();
        }

        public void Update(WorkoutComments model)
        {
            _db.workoutComments.Update(model);
        }
    }
}
