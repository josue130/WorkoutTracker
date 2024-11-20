using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Result;

namespace Workout.Application.Errors
{
    public static class JWTError
    {
        public static readonly Error JwtTokenInvalid = new Error(
            "Token", "Jwt token invalid");

    }

}
