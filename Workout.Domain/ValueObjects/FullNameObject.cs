using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Domain.Constants;

namespace Workout.Domain.ValueObjects
{
    public class FullNameObject
    {
        public string Value { get; }

        public FullNameObject(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(ErrorMessages.FullNameCannotBeEmpty);

            Value = value;
        }
    }
}
