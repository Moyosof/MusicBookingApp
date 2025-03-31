using MusicBookingApp.Application.Features.Events.Command.CreateEvent;
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
                TicketPrice = request.TicketPrice
            };
        }

        public static CreateEventResponse ToCreateEventResponse(Event @event)
        {
            return new CreateEventResponse { EventId = @event.Id };
        }
    }
}
