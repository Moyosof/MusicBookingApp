using MusicBookingApp.Domain.Entities.Base;

namespace MusicBookingApp.Domain.Entities
{
    public class Event : BaseEntity
    {
        public required string Name { get; set; }
        public required string ArtistId { get; set; }
        public Artist Artist { get; set; } = null!;
        public required string Location { get; set; }
        public DateTime EventDate { get; set; }
        public int MaxAttendees { get; set; }
        public decimal TicketPrice { get; set; }

        public List<Booking> Bookings { get; set; } = new();

    }
}
