using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace MusicBookingApp.Domain.Entities
{
    public class Artist : IdentityUser
    {
        public required string UserId { get; set; }
        public User User { get; set; } = null!;
        public required string StageName { get; set; }
        public required string Genre { get; set; }
        public required string Bio { get; set; }
        public bool IsAvailable { get; set; } = true;

    }
}
