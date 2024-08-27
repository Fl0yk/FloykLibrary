using FloykLibrary.Domain.Entities;
using System.Security.Claims;

namespace FloykLibrary.Application.Shared.Abstractions
{
    public interface IJwtProvider
    {
        public string GenerateJwt(User user);

        public string GenerateRefreshToken();

        public ClaimsPrincipal? GetClaimsPrincipal(string token);
    }
}
