using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Domain.Constants;

namespace Workout.Domain.ValueObjects
{
    public record EmailObject
    {
        public string Value { get; }
        public EmailObject(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !System.Text.RegularExpressions.Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) 
            {
                throw new ArgumentException(ErrorMessages.InvalidEmailFormat);
            }
               
            Value = value;
        }


    }
}
