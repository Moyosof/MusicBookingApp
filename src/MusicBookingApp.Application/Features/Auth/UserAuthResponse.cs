namespace MusicBookingApp.Application.Features.Auth
{
    public class UserAuthResponse
    {
        public required string Id { get; init; }
        public required string Role { get; init; }
        public string? AccessToken { get; init; }
        public string? RefreshToken { get; init; }
    }
}
