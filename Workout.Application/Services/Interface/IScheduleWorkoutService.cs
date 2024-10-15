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
    public interface IScheduleWorkoutService
    {
        Task<IEnumerable<ScheduleWorkoutDto>> GetScheduleWorkoutsByUserId(ClaimsPrincipal user);
        Task SetWorkoutSchedule(ScheduleWorkoutDto model, ClaimsPrincipal user);
        Task UpdateScheduledWorkout(ScheduleWorkoutDto model, ClaimsPrincipal user);
        Task DeleteScheduledWorkout(Guid scheduleWorkoutId, ClaimsPrincipal user);

    }
}
