using FluentValidation;
using MediatR;
using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Application.Extensions;
using MusicBookingApp.Application.Repositories.Base;
using MusicBookingApp.Domain.ServiceErrors;

namespace MusicBookingApp.Application.Features.Events.Queries.GetEventById
{
    public class GetEventByIdRequestDto
    {
        public string EventId { get; set; } = null!;
    }

    public class GetEventByIdRequest : IRequest<Result<GetEventResponse>>
    {
        public string UserId { get; set; } = null!;
        public string EventId { get; set; } = null!;
    }

    public class GetEventByIdRequestHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetEventByIdRequestHandler> logger,
        IValidator<GetEventByIdRequest> validator
    ) : IRequestHandler<GetEventByIdRequest, Result<GetEventResponse>>
    {
        public async Task<Result<GetEventResponse>> Handle(
            GetEventByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            logger.LogInformation("Request to retrieve event by Id: {id}.", request.EventId);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Validation failed for user: {UserId}", request.UserId);
                return Result<GetEventResponse>.Failure(validationResult.ToErrorList());
            }

            var @event = await unitOfWork.Events.GetEventByIdAsNoTrackingAsync(request.EventId);

            if (@event is null)
            {
                logger.LogWarning("Event not found: {Id}", request.EventId);
                return Result<GetEventResponse>.Failure(Errors.General.EventNotFound);
            }

            logger.LogInformation("Retrieved event for user: {userId}.", request.UserId);
            return Result<GetEventResponse>.Success(EventMapper.ToGetEventResponse(@event));
        }
    }

    public class Validation : AbstractValidator<GetEventByIdRequest>
    {
        public Validation()
        {
            RuleFor(x => x.EventId).NotEmpty();
        }
    }
}
