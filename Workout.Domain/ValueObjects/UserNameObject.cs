using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Domain.Constants;

namespace Workout.Domain.ValueObjects
{
    public record UserNameObject
    {
        public string Value { get; }

        public UserNameObject(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                throw new ArgumentException(ErrorMessages.UserNameCharacters);

            Value = value;
        }
    }
}
