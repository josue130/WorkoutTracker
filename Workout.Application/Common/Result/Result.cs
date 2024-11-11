using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout.Application.Common.Result
{
    public class Result
    {
        protected Result(bool isSuccess, Error error, object values)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
            Values = values;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Object Values { get; }

        public Error Error { get; }
        public static Result Success(Object value) => new(true, Error.None,value);

        public static Result Success() => new(true, Error.None, "");
        
        public static Result Failure(Error error) => new(false, error, "");
    }
}
