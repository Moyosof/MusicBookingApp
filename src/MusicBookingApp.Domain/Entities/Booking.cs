using MusicBookingApp.Domain.Entities.Base;
using MusicBookingApp.Domain.Enums;


namespace MusicBookingApp.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public required string UserId { get; set; }
        public User User { get; set; } = null!;
        public required string EventId { get; set; }
        public required DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public Event Event { get; set; } = null!;
    }
}
