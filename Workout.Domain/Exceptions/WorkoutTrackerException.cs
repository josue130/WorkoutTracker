using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout.Domain.Exceptions
{
    public abstract class WorkoutTrackerException : Exception
    {
        protected WorkoutTrackerException(string message) : base(message)
        {

        }
    }
}
