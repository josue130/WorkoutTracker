using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout.Application.Common.Dto
{
    public class ScheduleWorkoutDto
    {
        public Guid? Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public Guid WorkoutId { get; set; }
    }
}
