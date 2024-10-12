using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Domain.Entities;

namespace Workout.Application.Common.Interfaces
{
    public interface IScheduleWorkoutRepository : IRepository<ScheduleWorkout>
    {
        Task<IEnumerable<ScheduleWorkout>> GetScheduleWorkouts(Guid UserId);
        void Update(ScheduleWorkout model);
    }
}
