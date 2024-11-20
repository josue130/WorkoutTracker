using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;
using Workout.Application.Common.Result;

namespace Workout.Application.Services.Interface
{
    public interface IAuthService
    {
        Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequest);
        Task<Result<string>> Register(RegisterRequestDto request);
        Result<Guid> GetUserId(ClaimsPrincipal user);
    }
}
