using WorkoutApi.Models;

namespace WorkoutApi.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User applicationUser);
    }
}
