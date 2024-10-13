using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;
using Workout.Domain.Entities;

namespace Workout.Application.Services.Interface
{
    public interface IScheduleWorkoutService
    {
        Task<IEnumerable<ScheduleWorkoutDto>> GetScheduleWorkoutsByUserId(Guid userId);
        Task SetWorkoutSchedule(ScheduleWorkoutDto model);
        Task UpdateScheduledWorkout(ScheduleWorkoutDto model);
        Task DeleteScheduledWorkout(Guid scheduleWorkoutId);

    }
}
