using FluentValidation;
using MediatR;
using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Application.Extensions;
using MusicBookingApp.Application.Repositories.Base;
using MusicBookingApp.Application.Utility;
using MusicBookingApp.Domain.ServiceErrors;

namespace MusicBookingApp.Application.Features.User.Command.UpdateArtistProfile
{
    public class UpdateArtistProfileRequestDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StageName { get; set; }
        public string? Genre { get; set; }
        public string? Bio { get; set; }
    }

    public class UpdateArtistProfileRequest : IRequest<Result<MyUnit>>
    {
        public required string UserId { get; set; }
        public required string? FirstName { get; set; }
        public required string? LastName { get; set; }
        public required string? PhoneNumber { get; set; }
        public required string? StageName { get; set; }
        public required string? Genre { get; set; }
        public required string? Bio { get; set; }
    }

    public class UpdateArtistProfileRequestHandler(
    ILogger<UpdateArtistProfileRequestHandler> logger,
        IUnitOfWork unitOfWork, IValidator<UpdateArtistProfileRequest> validator
     ) : IRequestHandler<UpdateArtistProfileRequest, Result<MyUnit>>
    {
        public async Task<Result<MyUnit>> Handle(UpdateArtistProfileRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Attempting to update artist profile for user Id {userId}", request.UserId);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ToErrorList();
                logger.LogError("Validation errors occurred while updating user profile with request: {request}. Errors: {errors}.",
                                request,
                                errors);
                return Result<MyUnit>.Failure(errors);
            }


            var artist = await unitOfWork.Artists.GetArtistByUserIdAsync(request.UserId, cancellationToken);

            if (artist is null)
            {
                logger.LogWarning("Artist not found: {UserId}", request.UserId);
                return Result<MyUnit>.Failure(Errors.User.NotFound);
            }

            artist.User.FirstName = request.FirstName ?? artist.User.FirstName;
            artist.User.LastName = request.LastName ?? artist.User.LastName;
            artist.User.PhoneNumber = request.PhoneNumber ?? artist.User.PhoneNumber;
            artist.Genre = request.Genre ?? artist.Genre;
            artist.StageName = request.StageName ?? artist.StageName;
            artist.Bio = request.Bio ?? artist.Bio;

            unitOfWork.Artists.Update(artist);
            await unitOfWork.CompleteAsync();


            logger.LogInformation("Successfully updated artist details for user: {userId}.", request.UserId);
            return Result<MyUnit>.Success(MyUnit.Value);
        }
    }

    public class UpdateArtistProfileRequestValidator : AbstractValidator<UpdateArtistProfileRequest>
    {
        public UpdateArtistProfileRequestValidator()
        {
            RuleFor(x => x.UserId)
           .NotEmpty();
        }
    }
}
