using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Result;

namespace Workout.Application.Errors
{
    public static class WorkoutPlanError
    {
        public static readonly Error InvalidInputs = new Error(
            "Validation", "Inputs cannot be empty");
        public static readonly Error WorkoutPlanNameAlreadyExists = new Error(
            "Conflict", "Workout plan name already exits");
    }
}
