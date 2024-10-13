using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;

namespace Workout.Application.Services.Interface
{
    public interface IWorkoutExerciseService
    {
        Task<IEnumerable<WorkoutExerciseDto>> GetWorkoutExerciseById(Guid workoutExerciseId);
        Task AddWorkoutExercise(WorkoutExerciseDto model);
        Task UpdateWorkoutExercise(WorkoutExerciseDto model);
        Task DeleteWorkoutExercise(Guid workoutExerciseId);

    }
}
