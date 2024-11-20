using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Result;

namespace Workout.Application.Errors
{
    public static class ScheduleWorkoutError
    {
        public static readonly Error InvalidInputs = new Error(
            "Validation", "Inputs cannot be empty");
        public static readonly Error InvalidDate = new Error(
            "Validation", "The scheduled date cannot be in the past");
        public static readonly Error ScheduleNotFound = new Error(
            "NotFound", "Schedule not found");
    }
}
