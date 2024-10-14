﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Domain.Constants;

namespace Workout.Domain.Exceptions
{
    public class UserNameOrPasswordIncorrectException : WorkoutTrackerException
    {
        public UserNameOrPasswordIncorrectException() : base(ErrorMessages.UserNameOrPasswordIncorrect)
        {
        }
    }
}
