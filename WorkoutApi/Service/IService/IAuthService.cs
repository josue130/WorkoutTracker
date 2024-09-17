using Microsoft.AspNetCore.Identity.Data;
using WorkoutApi.Models.Dto;

namespace WorkoutApi.Service.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequest);
        Task<string> Register(RegisterRequestDto request);
    }
}
