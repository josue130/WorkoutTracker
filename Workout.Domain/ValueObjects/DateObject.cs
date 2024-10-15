using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Domain.Constants;

namespace Workout.Domain.ValueObjects
{
    public class DateObject
    {
        public DateTime Value { get; }

        public DateObject(DateTime value)
        {
            if (value < DateTime.Today)
                throw new ArgumentException(ErrorMessages.InvalidDate);

            Value = value;
        }
    }
}
