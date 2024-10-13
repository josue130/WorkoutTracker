using Workout.Domain.Entities;

namespace Workout.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User applicationUser);
    }
}
