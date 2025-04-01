using MusicBookingApp.Application.Features.Events.Command.CreateEvent;
using MusicBookingApp.Application.Features.Events.Queries;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Features.Events
{
    public static class EventMapper
    {
        public static Event ToCreateEventRequest(CreateEventRequest request, string artistId)
        {
            return new Event
            {
                Name = request.EventName,
                ArtistId = artistId,
                Location = request.Location,
                MaxAttendees = request.MaxAttendees,
                EventDate = request.EventDate,
                TicketPrice = request.TicketPrice,
            };
        }

        public static CreateEventResponse ToCreateEventResponse(Event @event)
        {
            return new CreateEventResponse { EventId = @event.Id };
        }

        public static GetEventResponse ToGetEventResponse(Event @event)
        {
            return new GetEventResponse
            {
                EventId = @event.Id,
                Location = @event.Location,
                Name = @event.Name,
                EventDate = @event.EventDate,
                MaxAttendees = @event.MaxAttendees,
                TicketPrice = @event.TicketPrice,
                ArtistName = $"{@event.Artist.User.FirstName} {@event.Artist.User.FirstName}",
                Bookings = @event
                    .Bookings.OrderByDescending(b => b.BookingDate)
                    .Select(EventMapper.ToGetEventBookingResponse)
                    .ToList(),
            };
        }

        public static GetEventBookingResponse ToGetEventBookingResponse(Booking booking)
        {
            return new GetEventBookingResponse
            {
                BookingId = booking.Id,
                BookieName = booking.BookieName,
                BookieEmail = booking.BookieEmail,
                BookingDate = booking.BookingDate,
                Status = booking.Status.ToString(),
            };
        }
    }
}
