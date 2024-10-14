using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;
using Workout.Domain.Entities;

namespace Workout.Application.Services.Interface
{
    public interface IWorkoutCommentsService
    {
        Task<IEnumerable<WorkoutCommentsDto>> GetWorkoutCommentsByWorkoutId(Guid workoutId, ClaimsPrincipal user);
        Task AddWorkoutComment(WorkoutCommentsDto model,ClaimsPrincipal user);
        Task UpdateWorkoutComment(WorkoutCommentsDto model, ClaimsPrincipal user);
        Task DeleteWorkoutComment(Guid workoutCommentId, ClaimsPrincipal user);
    }
}
