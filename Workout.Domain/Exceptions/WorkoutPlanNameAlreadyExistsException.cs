﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Domain.Constants;

namespace Workout.Domain.Exceptions
{
    public class WorkoutPlanNameAlreadyExistsException : WorkoutTrackerException
    {
        public WorkoutPlanNameAlreadyExistsException() : base(ErrorMessages.WorkoutPlanNameAlreadyExists)
        {
        }
    }
}
