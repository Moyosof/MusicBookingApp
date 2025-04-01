namespace MusicBookingApp.Application.Features.Events.Queries
{
    public class GetEventResponse
    {
        public required string EventId { get; set; }
        public required string Name { get; set; }
        public required string ArtistName { get; set; }
        public required string Location { get; set; }
        public DateTime EventDate { get; set; }
        public int MaxAttendees { get; set; }
        public decimal TicketPrice { get; set; }

        public List<GetEventBookingResponse> Bookings { get; set; } = new();
    }

    public class GetEventBookingResponse
    {
        public required string BookingId { get; set; }
        public required string BookieName { get; set; }
        public required string BookieEmail { get; set; }
        public required DateTime BookingDate { get; set; }
        public required string Status { get; set; }
    }
}
