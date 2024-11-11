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
    public interface IScheduleWorkoutService
    {
        Task<Result> GetScheduleWorkoutsByUserId(ClaimsPrincipal user);
        Task<Result> SetWorkoutSchedule(ScheduleWorkoutDto model, ClaimsPrincipal user);
        Task<Result> UpdateScheduledWorkout(ScheduleWorkoutDto model, ClaimsPrincipal user);
        Task<Result> DeleteScheduledWorkout(Guid scheduleWorkoutId, ClaimsPrincipal user);

    }
}
