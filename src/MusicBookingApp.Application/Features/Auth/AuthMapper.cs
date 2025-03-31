using MusicBookingApp.Application.Extensions;
using MusicBookingApp.Application.Features.Auth.Command.Register;
using MusicBookingApp.Application.Features.User.Queries.GetArtistById;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Features.Auth
{
    public static class AuthMapper
    {
        public static Domain.Entities.User MapToUser(RegisterRequest request)
        {
            return new Domain.Entities.User
            {
                FirstName = request.FirstName.FirstCharToUpper(),
                LastName = request.LastName.FirstCharToUpper(),
                Email = request.EmailAddress,
                UserName = request.EmailAddress,
                Role = request.Role,
                PhoneNumber = request.PhoneNumber,
                IsActive = true
            };
        }

        public static GetArtistByUserIdResponse ToGetUserResponse(Artist artist)
        {
            return new GetArtistByUserIdResponse
            {
                ArtistId = artist.Id,
                FirstName = artist.User.FirstName,
                LastName = artist.User.LastName,
                Email = artist.User.Email!,
                Role = artist.User.Role,
                PhoneNumber = artist.User.PhoneNumber!,
                Bio = artist.Bio,
                Genre = artist.Genre,
                StageName = artist.StageName,
                IsActive = artist.User.IsActive
            };
        }
    }
}
