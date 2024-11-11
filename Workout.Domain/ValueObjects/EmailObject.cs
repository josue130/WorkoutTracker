using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout.Domain.ValueObjects
{
    public record EmailObject
    {
        public string Value { get; init; }
        private EmailObject(string value) => Value = value;
        public static EmailObject? Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !System.Text.RegularExpressions.Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {

                return null;

            }

            return new EmailObject(value);
        }


    }
}
