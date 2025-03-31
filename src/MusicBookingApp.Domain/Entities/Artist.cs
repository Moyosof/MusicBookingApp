using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MusicBookingApp.Domain.Entities.Base;

namespace MusicBookingApp.Domain.Entities
{
    public class Artist : BaseEntity
    {
        public required string UserId { get; set; }
        public User User { get; set; } = null!;
        public required string StageName { get; set; }
        public required string Genre { get; set; }
        public required string Bio { get; set; }
        public bool IsAvailable { get; set; } = true;

        // Navigation property for events the artist is performing at
        public List<Event> Events { get; set; } = new();
    }
}
