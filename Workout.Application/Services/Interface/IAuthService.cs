using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout.Application.Common.Dto;

namespace Workout.Application.Services.Interface
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequest);
        Task<string> Register(RegisterRequestDto request);
    }
}
