using FluentValidation;
using MediatR;
using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Application.Extensions;
using MusicBookingApp.Application.Features.Auth;
using MusicBookingApp.Application.Repositories.Base;
using MusicBookingApp.Domain.ServiceErrors;

namespace MusicBookingApp.Application.Features.User.Queries.GetArtistById
{
    public class GetArtistByUserIdRequest : IRequest<Result<GetArtistByUserIdResponse>>
    {
        public string UserId { get; set; } = null!;
    }

    public class GetUserByIdRequestHandler(IUnitOfWork unitOfWork,
    ILogger<GetUserByIdRequestHandler> logger,
    IValidator<GetArtistByUserIdRequest> validator) : IRequestHandler<GetArtistByUserIdRequest, Result<GetArtistByUserIdResponse>>
    {
        public async Task<Result<GetArtistByUserIdResponse>> Handle(GetArtistByUserIdRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Request to retrieve user details: {userId}.", request.UserId);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Validation failed for user: {UserId}", request.UserId);
                return Result<GetArtistByUserIdResponse>.Failure(validationResult.ToErrorList());
            }

            var artist = await unitOfWork.Artists.GetArtistByUserIdAsNoTrackingAsync(request.UserId, cancellationToken);

            if (artist is null)
            {
                logger.LogWarning("User not found: {UserId}", request.UserId);
                return Result<GetArtistByUserIdResponse>.Failure(Errors.Auth.UserNotFound);
            }

            logger.LogInformation("Retrieved user details for user: {userId}.", request.UserId);
            return Result<GetArtistByUserIdResponse>.Success(AuthMapper.ToGetUserResponse(artist));
        }
    }

    public class GetArtistByUserIdRequestValidator : AbstractValidator<GetArtistByUserIdRequest>
    {
        public GetArtistByUserIdRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
