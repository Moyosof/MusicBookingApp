using FluentValidation;
using MediatR;
using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Application.Extensions;
using MusicBookingApp.Application.Repositories.Base;
using MusicBookingApp.Domain.ServiceErrors;

namespace MusicBookingApp.Application.Features.Events.Command.CreateEvent
{
    public class CreateEventRequestDto
    {
        public required string EventName { get; set; }
        public required string Location { get; set; }
        public required DateTime EventDate { get; set; }
        public required int MaxAttendees { get; set; }
        public required decimal TicketPrice { get; set; }
    }
    public class CreateEventRequest : IRequest<Result<CreateEventResponse>>
    {
        public required string UserId { get; set; }
        public required string EventName { get; set; }
        public required string Location { get; set; }
        public required DateTime EventDate { get; set; }
        public required int MaxAttendees { get; set; }
        public required decimal TicketPrice { get; set; }
    }


    public class CreateEventRequestHandler(
        ILogger<CreateEventRequestHandler> logger,
        IUnitOfWork unitOfWork, IValidator<CreateEventRequest> validator
     ) : IRequestHandler<CreateEventRequest, Result<CreateEventResponse>>
    {
        public async Task<Result<CreateEventResponse>> Handle(CreateEventRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Attempting to create artist event for user Id {userId}", request.UserId);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ToErrorList();
                logger.LogError("Validation errors occurred while creating artist event with request: {request}. Errors: {errors}.",
                                request,
                                errors);
                return Result<CreateEventResponse>.Failure(errors);
            }

            var artist = await unitOfWork.Artists.GetArtistByUserIdAsync(request.UserId, cancellationToken);
            if (artist is null)
            {
                logger.LogWarning("Artist not found with Id: {UserId}", request.UserId);
                return Result<CreateEventResponse>.Failure(Errors.User.NotFound);
            }
            var isEventNameExist = await unitOfWork.Events.EventNameExistsAsync(artist.Id, request.EventName);
            if (isEventNameExist)
            {
                logger.LogError("An event with the same name already exist for this userId: { UserId}", request.UserId);
                return Result<CreateEventResponse>.Failure(Errors.General.EventNameExist);
            }
            var savingsPlan = EventMapper.ToCreateEventRequest(request, artist.Id);

            await unitOfWork.Events.Add(savingsPlan);
            await unitOfWork.CompleteAsync();

            logger.LogInformation("Successfully created artist event for user: {userId}.", request.UserId);
            return Result<CreateEventResponse>.Success(EventMapper.ToCreateEventResponse(savingsPlan));
        }

        public class CreateEventRequestValidation : AbstractValidator<CreateEventRequest>
        {
            public CreateEventRequestValidation()
            {
                RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("UserId is required.");

                RuleFor(x => x.EventName)
                    .NotEmpty();

                RuleFor(x => x.MaxAttendees).NotEmpty()
                    .GreaterThan(0).WithMessage("WeeklyAmount must be greater than 0.");

                RuleFor(x => x.EventDate).NotEmpty()
                    .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Start date can not be in the past, It can be now or future.");

                RuleFor(x => x.TicketPrice).NotEmpty()
                    .GreaterThan(0).WithMessage("TargetAmount must be greater than 0.");
            }
        }
    }
}
