using MusicBookingApp.Application.Extensions;
using MusicBookingApp.Application.Features.Auth.Command.Register;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Features.Auth
{
    public static class AuthMapper
    {
        public static User MapToUser(RegisterRequest request)
        {
            return new User
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
    }
}
