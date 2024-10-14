using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Workout.Domain.Constants
{
    public static class ErrorMessages
    {
        public const string UserNameAlreadyExists = "Username already exits";
        public const string UserNameOrPasswordIncorrect = "User name or password is incorrect";
        public const string WorkoutPlanNameAlreadyExists = "Workout plan name already exits";
        public const string InvalidEmailFormat = "Invalid email format";
        public const string FullNameCannotBeEmpty = "Full name cannot be empty";
        public const string UserNameCharacters = "Username must be at least 3 characters long";
        public const string CommentsCannotBeEmpty = "Comment cannot be empty";
    }
}
