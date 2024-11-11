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
        Task<Result> AddWorkoutPlan(WorkoutPlanDto model, ClaimsPrincipal user);
        Task<Result> UpdateWorkoutPlan(WorkoutPlanDto model, ClaimsPrincipal user);
        Task<Result> DeleteWorkoutPlan(Guid workoutPlanId, ClaimsPrincipal user);
        Task<Result> GetByWorkouPlanId(Guid workoutPlanId, ClaimsPrincipal user);
        Task<Result> GenerateReport(ClaimsPrincipal user);
        Task<Result> GetWorkoutsbyUserId(ClaimsPrincipal user);


    }
}
