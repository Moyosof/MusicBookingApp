using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Domain.ServiceErrors;
using MusicBookingApp.Domain.Settings;

namespace MusicBookingApp.Application.Features.Auth.Command.Login
{
    public class LoginRequest : IRequest<Result<UserAuthResponse>>
    {
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginRequestHandler(
        UserManager<User> userManager,
        IOptionsSnapshot<JwtSettings> options,
        SignInManager<User> signInManager,
        ILogger<LoginRequestHandler> logger
    ) : IRequestHandler<LoginRequest, Result<UserAuthResponse>>
    {
        private readonly JwtSettings _jwtSettings = options.Value;

        public async Task<Result<UserAuthResponse>> Handle(
            LoginRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await userManager.FindByEmailAsync(request.EmailAddress);
            if (user is null)
            {
                logger.LogWarning(
                    "Email not found during login: {EmailAddress}.",
                    request.EmailAddress
                );
                return Result<UserAuthResponse>.Failure(Errors.Auth.LoginFailed);
            }

            var signInResult = await signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                true
            );
            if (signInResult.Succeeded)
            {


                logger.LogInformation("User {userId} logged in successfully.", user.Id);
                return Result<UserAuthResponse>.Success(
                    new UserAuthResponse
                    {
                        Id = user.Id,
                        Role = user.Role,
                        AccessToken = GenerateUserToken(
                            user.Email,
                            user.Role,
                            user.Id,
                            user.FirstName
                        ),
                        RefreshToken = GenerateRefreshToken(),
                    }
                );
            }

            if (signInResult.IsLockedOut)
            {
                logger.LogInformation(
                    "User {userId} is locked out. End date: {lockoutEnd}.\n\tRequest: {request}",
                    user.Id,
                    user.LockoutEnabled,
                    JsonSerializer.Serialize(request)
                );
                return Result<UserAuthResponse>.Failure(Errors.User.IsLockedOut);
            }

            if (signInResult.IsNotAllowed)
            {
                logger.LogInformation(
                    "User {userId} is not allowed to access the system.\n\tRequest: {request}",
                    user.Id,
                    JsonSerializer.Serialize(request)
                );
                return Result<UserAuthResponse>.Failure(Errors.User.IsNotAllowed);
            }

            logger.LogError(
                "Login failed for user {userId}.\n\tRequest: {request}",
                user.Id,
                JsonSerializer.Serialize(request)
            );
            return Result<UserAuthResponse>.Failure(Errors.Auth.LoginFailed);
        }

        private string GenerateUserToken(
            string emailAddress,
            string userRole,
            string userId,
            string firstName
        )
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey!)
            );

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Email, emailAddress),
                new(JwtRegisteredClaimNames.NameId, userId),
                new(ClaimTypes.Role, userRole),
                new(JwtRegisteredClaimNames.Name, firstName),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(
                    Convert.ToDouble(_jwtSettings.TokenLifeTimeInHours)
                ),
                SigningCredentials = new SigningCredentials(
                    signingKey,
                    SecurityAlgorithms.HmacSha256
                ),
                Subject = new ClaimsIdentity(claims),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}