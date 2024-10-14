using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout.Domain.ValueObjects
{
    public record WorkoutPlanNameObject
    {
        public string value { get; }
        public WorkoutPlanNameObject(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"The name cannot be empty.");
            }
            value = name;
        }
    }
}
