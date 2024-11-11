using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout.Application.Common.Result
{
    public sealed record Error(string code, string message)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
