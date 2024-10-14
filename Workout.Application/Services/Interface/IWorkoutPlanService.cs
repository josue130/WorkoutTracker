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
    public interface IWorkoutPlanService
    {
        Task AddWorkoutPlan(WorkoutPlanDto model, ClaimsPrincipal user);
        Task UpdateWorkoutPlan(WorkoutPlanDto model, ClaimsPrincipal user);
        Task DeleteWorkoutPlan(Guid workoutPlanId, ClaimsPrincipal user);
        Task<WorkoutPlanDto> GetByWorkouPlanId(Guid workoutPlanId, ClaimsPrincipal user);
        Task<IEnumerable<WorkoutPlanResponseDto>> GenerateReport(ClaimsPrincipal user);
        Task<IEnumerable<WorkoutPlanResponseDto>> GetWorkoutsbyUserId(ClaimsPrincipal user);


    }
}
