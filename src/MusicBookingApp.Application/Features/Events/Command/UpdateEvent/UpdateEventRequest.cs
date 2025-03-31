using FluentValidation;
using MediatR;
using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Application.Extensions;
using MusicBookingApp.Application.Repositories.Base;
using MusicBookingApp.Application.Utility;
using MusicBookingApp.Domain.ServiceErrors;

namespace MusicBookingApp.Application.Features.Events.Command.UpdateEvent
{
    public class UpdateEventRequestDto
    {
        public string? EventName { get; set; }
        public string? Location { get; set; }
        public DateTime? EventDate { get; set; }
        public int? MaxAttendees { get; set; }
        public decimal? TicketPrice { get; set; }
    }
    public class UpdateEventRequest : IRequest<Result<MyUnit>>
    {
        public required string UserId { get; set; }
        public required string EventId { get; set; }
        public required string? EventName { get; set; }
        public required string? Location { get; set; }
        public required DateTime? EventDate { get; set; }
        public required int? MaxAttendees { get; set; }
        public required decimal? TicketPrice { get; set; }
    }


    public class UpdateEventRequestHandler(
        ILogger<UpdateEventRequestHandler> logger,
        IUnitOfWork unitOfWork, IValidator<UpdateEventRequest> validator
     ) : IRequestHandler<UpdateEventRequest, Result<MyUnit>>
    {
        public async Task<Result<MyUnit>> Handle(UpdateEventRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Attempting to updating artist event for user Id {userId}", request.UserId);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ToErrorList();
                logger.LogError("Validation errors occurred while updating00 artist event with request: {request}. Errors: {errors}.",
                                request,
                                errors);
                return Result<MyUnit>.Failure(errors);
            }

            var @event = await unitOfWork.Events.GetById(request.EventId);
            if (@event is null)
            {
                logger.LogWarning("Artist not found with Id: {UserId}", request.UserId);
                return Result<MyUnit>.Failure(Errors.User.NotFound);
            }

            @event.Name = request.EventName ?? @event.Name;
            @event.MaxAttendees = request.MaxAttendees ?? @event.MaxAttendees;
            @event.TicketPrice = request.TicketPrice ?? @event.TicketPrice;
            @event.Location = request.Location ?? @event.Location;
            @event.EventDate = request.EventDate ?? @event.EventDate;

            unitOfWork.Events.Update(@event);
            await unitOfWork.CompleteAsync();

            logger.LogInformation("Successfully update artist event for user: {userId}.", request.UserId);
            return Result<MyUnit>.Success(MyUnit.Value);
        }

        public class CreateEventRequestValidation : AbstractValidator<UpdateEventRequest>
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