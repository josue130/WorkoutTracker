using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Result;

namespace Workout.Application.Services.Interface
{
    public interface IAuthService
    {
        Task<Result> Login(LoginRequestDto loginRequest);
        Task<Result> Register(RegisterRequestDto request);
    }
}
