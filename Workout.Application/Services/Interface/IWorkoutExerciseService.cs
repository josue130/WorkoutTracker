﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Result;

namespace Workout.Application.Services.Interface
{
    public interface IWorkoutExerciseService
    {
        Task<Result> GetWorkoutExerciseById(Guid workoutExerciseId, ClaimsPrincipal user);
        Task<Result> AddWorkoutExercise(WorkoutExerciseDto model, ClaimsPrincipal user);
        Task<Result> UpdateWorkoutExercise(WorkoutExerciseDto model, ClaimsPrincipal user);
        Task<Result> DeleteWorkoutExercise(Guid workoutExerciseId, ClaimsPrincipal user);

    }
}
