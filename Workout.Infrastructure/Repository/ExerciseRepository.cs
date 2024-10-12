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
    public class ExerciseRepository : Repository<Exercise>, IExerciseRepository
    {
        private readonly AppDbContext _db;
        public ExerciseRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Exercise model)
        {
            _db.exercises.Update(model);
        }
    }
}
