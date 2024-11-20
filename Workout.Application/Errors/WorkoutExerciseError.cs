using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Result;

namespace Workout.Application.Errors
{
    public static class WorkoutExerciseError
    {

        public static readonly Error InvalidInputs = new Error(
            "Validation", "Inputs cannot be empty");

        public static readonly Error InvalidSets = new Error(
            "Validation", "Sets must be greater than 0.");

        public static readonly Error InvalidRepetitions = new Error(
            "Validation", "Repetitions must be greater than 0");

        public static readonly Error InvalidWeight = new Error(
            "Validation", "Weight must be non-negative.");

        public static readonly Error WorkoutExerciseNotFound = new Error(
            "NotFound", "Workout exercise not found");
    }
}
