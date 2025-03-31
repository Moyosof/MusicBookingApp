using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Identity;

using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Application.Extensions;
using MusicBookingApp.Domain.Constants;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Domain.ServiceErrors;

namespace MusicBookingApp.Application.Features.Auth.Command.Register
{
    public class RegisterRequest : IRequest<Result<UserAuthResponse>>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }

    public class RegisterRequestHandler(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<RegisterRequestHandler> logger,
        IValidator<RegisterRequest> validator)
        : IRequestHandler<RegisterRequest, Result<UserAuthResponse>>
    {
        public async Task<Result<UserAuthResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Registration request received for email: {emailAddress}.", request.EmailAddress);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Validation failed for new user: {emailAddress}", request.EmailAddress);
                return Result<UserAuthResponse>.Failure(validationResult.ToErrorList());
            }

            var existingUser = await userManager.FindByEmailAsync(request.EmailAddress);
            if (existingUser is not null)
            {
                logger.LogWarning("Duplicate email found during registration: {emailAddress}", request.EmailAddress);
                return Result<UserAuthResponse>.Failure(Errors.User.DuplicateEmail);
            }

            var newUser = AuthMapper.MapToUser(request);
            var createUserResult = await userManager.CreateAsync(newUser, request.Password);
            if (!createUserResult.Succeeded)
            {
                var errors = createUserResult.Errors
                    .Select(error => Error.Validation("User." + error.Code, error.Description))
                    .ToList();

                logger.LogError("User registration failed for email: {emailAddress}. Errors: {errors}",
                    request.EmailAddress, string.Join(", ", errors.Select(e => $"{e.Code}: {e.Description}")));

                return Result<UserAuthResponse>.Failure(errors);
            }

            try
            {
                if (!await roleManager.RoleExistsAsync(request.Role))
                {
                    var roleCreationResult = await roleManager.CreateAsync(new IdentityRole(request.Role));
                    if (!roleCreationResult.Succeeded)
                    {
                        logger.LogWarning("Failed to create role for user: {emailAddress}", request.EmailAddress);
                        await userManager.DeleteAsync(newUser);
                        return Result<UserAuthResponse>.Failure(Errors.User.FailedToCreateRole);
                    }
                }

                await userManager.AddToRoleAsync(newUser, request.Role);
                logger.LogInformation("User registered successfully: {emailAddress}.", request.EmailAddress);

                var emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var emailConfirmationResult = await userManager.ConfirmEmailAsync(newUser, emailConfirmationToken);

                if (!emailConfirmationResult.Succeeded)
                {
                    logger.LogError("Email verification failed for user: {emailAddress}", request.EmailAddress);
                    await userManager.DeleteAsync(newUser);
                    return Result<UserAuthResponse>.Failure(Errors.Auth.EmailVerificationFailed);
                }

                return Result<UserAuthResponse>.Success(new UserAuthResponse
                {
                    Id = newUser.Id,
                    Role = newUser.Role
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "User registration failed for email: {emailAddress}. Rolling back user creation.", request.EmailAddress);
                await userManager.DeleteAsync(newUser);
                throw;
            }
        }
    }


    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x)
                .NotEmpty();

            RuleFor(x => x.FirstName).ValidateFirstName();
            RuleFor(x => x.LastName).ValidateLastName();
            RuleFor(x => x.EmailAddress).ValidateEmailAddress();

            RuleFor(x => x.Role)
                .Must(x => Roles.AllRoles.Contains(x))
                .WithMessage("These are the valid roles: " + string.Join(", ", Roles.AllRoles))
                .WithErrorCode("RegisterRequest.InvalidRole");
        }
    }
}