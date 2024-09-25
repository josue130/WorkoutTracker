using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WorkoutApi.Service
{
    public static class JwtHelper
    {
        private static IHttpContextAccessor? _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static Guid GetUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(userIdClaim?.Value, out Guid userId))
            {
                return userId;
            }
            else
            {
                throw new FormatException("The user ID format is invalid.");
            }
        }
    }
}
