using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout.Domain.ValueObjects
{
    public record WorkoutPlanDescriptionObject
    {

        public string value { get; }
        public WorkoutPlanDescriptionObject(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("The description cannot be empty.");
            }
            value = description;
        }
    }
}
