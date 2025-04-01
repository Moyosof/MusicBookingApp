namespace MusicBookingApp.Application.Features.Bookings.Command.BookAnEvent
{
    public class BookAnEventResponse
    {
        public required string BookingTicketId { get; set; }
        public required string Status { get; set; }
    }
}
