using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Result;
using Workout.Domain.Entities;

namespace Workout.Application.Services.Interface
{
    public interface IWorkoutCommentsService
    {
        Task<Result> GetWorkoutCommentsByWorkoutId(Guid workoutId, ClaimsPrincipal user);
        Task<Result> AddWorkoutComment(WorkoutCommentsDto model,ClaimsPrincipal user);
        Task<Result> UpdateWorkoutComment(WorkoutCommentsDto model, ClaimsPrincipal user);
        Task<Result> DeleteWorkoutComment(Guid workoutCommentId, ClaimsPrincipal user);
    }
}
