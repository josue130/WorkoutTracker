using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;
using Workout.Domain.Entities;

namespace Workout.Application.Services.Interface
{
    public interface IWorkoutPlanService
    {
        Task AddWorkoutPlan(WorkoutPlanDto model);
        Task UpdateWorkoutPlan(WorkoutPlanDto model);
        Task DeleteWorkoutPlan(Guid workoutPlanId);
        Task<WorkoutPlanDto> GetByWorkouPlanId(Guid workoutPlanId);
        Task<IEnumerable<WorkoutPlanResponseDto>> GenerateReport(Guid userId);
        Task<IEnumerable<WorkoutPlanResponseDto>> GetWorkoutsbyUserId(Guid userId);


    }
}
