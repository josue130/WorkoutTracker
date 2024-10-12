using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Domain.Entities;

namespace Workout.Application.Common.Interfaces
{
    public interface IWorkoutExercisesRepository : IRepository<WorkoutExercise>
    {
        Task<IEnumerable<WorkoutExercise>> GetWorkoutExercises(Guid workoutId);
        void Update(WorkoutExercise model);
    }
}
