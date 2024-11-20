using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout.Application.Common.Result
{
    public class Result<T>
    {
        private readonly T? _value;
        protected Result(bool isSuccess, T values)
        {
            IsSuccess = isSuccess;
            Error = Error.None;
            Values = values;
        }
        protected Result(bool isSuccess,Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public T Values 
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException("there is no value for failure");
                }
                return _value!;
            }
            private init => _value = value;
        }

        public Error Error { get; }
        public static Result<T> Success(T value) => new(true,value);
        
        public static Result<T> Failure(Error error) => new(false, error);
    }
}
