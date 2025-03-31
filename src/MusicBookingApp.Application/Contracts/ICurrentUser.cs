using System.Security.Claims;

namespace MusicBookingApp.Application.Contracts
{
    public interface ICurrentUser
    {
        string UserId { get; }
        string Email { get; }
        string Role { get; }
        ClaimsPrincipal User { get; }
    }
}