using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Result;

namespace Workout.Application.Errors
{
    public static class AuthError
    {
        public static readonly Error InvalidInputs = new Error(
            "Validation", "Inputs cannot be empty");
        public static readonly Error UserNameAlreadyExits = new Error(
        "Conflict", "Username already exits");

        public static readonly Error InvalidEmailFormat = new Error(
            "Validation", "Invalid email format");

        public static readonly Error UserNameNotExist = new Error(
            "Credentials", "The user does not exist");

        public static readonly Error IncorrectPassword = new Error(
            "Credentials", "Incorrect password");
    }
}
