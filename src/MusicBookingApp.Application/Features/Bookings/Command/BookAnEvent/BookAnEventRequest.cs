using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Application.Extensions;
using MusicBookingApp.Application.Repositories;
using MusicBookingApp.Application.Repositories.Base;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Domain.Enums;
using MusicBookingApp.Domain.ServiceErrors;

namespace MusicBookingApp.Application.Features.Bookings.Command.BookAnEvent
{
    public class BookAnEventRequestDto
    {
        public required string BookieName { get; set; }
        public required string BookieEmail { get; set; }
    }
    public class BookAnEventRequest : IRequest<Result<BookAnEventResponse>>
    {
        public required string EventId { get; set; }
        public required string BookieName { get; set; }
        public required string BookieEmail { get; set; }
    }

    public class BookAnEventRequestHandler(
        IUnitOfWork unitOfWork,
        ILogger<BookAnEventRequestHandler> logger,
        IValidator<BookAnEventRequest> validator
    ) : IRequestHandler<BookAnEventRequest, Result<BookAnEventResponse>>
    {
        public async Task<Result<BookAnEventResponse>> Handle(
            BookAnEventRequest request,
            CancellationToken cancellationToken
        )
        {
            logger.LogInformation("Request to book event by Id: {id}.", request.EventId);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Validation failed for user: {UserId}", request.EventId);
                return Result<BookAnEventResponse>.Failure(validationResult.ToErrorList());
            }

            var eventEntity = await unitOfWork.Events.GetById(request.EventId);
            if (eventEntity is null)
            {
                logger.LogWarning("Event with ID {EventId} not found.", request.EventId);
                return Result<BookAnEventResponse>.Failure(Errors.General.EventNotFound);
            }

            var existingBooking = await unitOfWork.Bookings.GetExistingBookingByEmailAsync(
            request.EventId,
            request.BookieEmail,
            cancellationToken
        );

            if (existingBooking is not null)
            {
                logger.LogWarning(
                    "User {BookieEmail} has already booked this event {EventId}.",
                    request.BookieEmail,
                    request.EventId
                );
                return Result<BookAnEventResponse>.Failure(Errors.General.AlreadyBooked);
            }

            // Get current booking count
            var currentAttendeesCount = await unitOfWork.Bookings.GetCurrentAttendeesCountAsync(request.EventId, cancellationToken);


            if (currentAttendeesCount >= eventEntity.MaxAttendees)
            {
                logger.LogWarning("Event {EventId} is fully booked.", request.EventId);
                return Result<BookAnEventResponse>.Failure(Errors.General.EventFullyBooked);
            }

            var booking = new Booking
            {
                BookieName = request.BookieName,
                BookieEmail = request.BookieEmail,
                EventId = request.EventId,
                BookingDate = DateTime.UtcNow,
                Status = BookingStatus.Confirmed,
            };

            await unitOfWork.Bookings.Add(booking);
            await unitOfWork.CompleteAsync();

            logger.LogInformation(
                "Booking successful for event {EventId} by {BookieName}.",
                request.EventId,
                request.BookieName
            );

            return Result<BookAnEventResponse>.Success(
                new BookAnEventResponse
                {
                    BookingTicketId = booking.Id,
                    Status = booking.Status.ToString(),
                }
            );
        }
    }

    public class Validation : AbstractValidator<BookAnEventRequest>
    {
        public Validation()
        {
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.BookieEmail).NotEmpty();
            RuleFor(x => x.BookieName).NotEmpty();
        }
    }
}
