using MusicBookingApp.Domain.Entities.Base;
using MusicBookingApp.Domain.Enums;

namespace MusicBookingApp.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public required string BookieName { get; set; }
        public required string BookieEmail { get; set; }
        public required string EventId { get; set; }
        public Event Event { get; set; } = null!;
        public required DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
    }
}
