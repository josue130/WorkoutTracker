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
    public interface IWorkoutPlanService
    {
        Task<Result<string>> AddWorkoutPlan(WorkoutPlanDto model, ClaimsPrincipal user);
        Task<Result<string>> UpdateWorkoutPlan(WorkoutPlanDto model, ClaimsPrincipal user);
        Task<Result<string>> DeleteWorkoutPlan(Guid workoutPlanId, ClaimsPrincipal user);
        Task<Result<WorkoutPlanDto>> GetByWorkouPlanId(Guid workoutPlanId, ClaimsPrincipal user);
        Task<Result<IEnumerable<WorkoutPlanResponseDto>>> GenerateReport(ClaimsPrincipal user);
        Task<Result<IEnumerable<WorkoutPlanResponseDto>>> GetWorkoutsbyUserId(ClaimsPrincipal user);
        Task<Result<WorkoutPlan>> CheckAccess(Guid? workoutPlanId, Guid userId);
        Task<WorkoutPlan?> CheckName(string name, Guid userId);


    }
}
