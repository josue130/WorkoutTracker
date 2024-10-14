using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;

namespace Workout.Application.Services.Interface
{
    public interface IWorkoutExerciseService
    {
        Task<IEnumerable<WorkoutExerciseDto>> GetWorkoutExerciseById(Guid workoutExerciseId, ClaimsPrincipal user);
        Task AddWorkoutExercise(WorkoutExerciseDto model, ClaimsPrincipal user);
        Task UpdateWorkoutExercise(WorkoutExerciseDto model, ClaimsPrincipal user);
        Task DeleteWorkoutExercise(Guid workoutExerciseId, ClaimsPrincipal user);

    }
}
