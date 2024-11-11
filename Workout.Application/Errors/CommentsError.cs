using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Result;

namespace Workout.Application.Errors
{
    public static class CommentsError
    {
        public static readonly Error CommentsCannotBeEmpty = new Error(
           "Validation", "Comment cannot be empty");
    }
}
