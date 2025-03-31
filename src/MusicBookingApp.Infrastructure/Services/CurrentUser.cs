using System.Security.Claims;

using MusicBookingApp.Application.Contracts;

namespace MusicBookingApp.Infrastructure.Services
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
    {
        public string UserId =>
            httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new InvalidOperationException("User not authorised.");

        public string Email =>
            httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value ?? throw new InvalidOperationException("User not authorised.");

        public string Role
        {
            get
            {
                var roleClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

                if (roleClaim != null)
                {
                    return roleClaim;
                }
                else
                {
                    throw new InvalidOperationException("User not authorized.");
                }
            }
        }

        public ClaimsPrincipal User => httpContextAccessor.HttpContext?.User ?? throw new InvalidOperationException("User not authorised.");
    }
}