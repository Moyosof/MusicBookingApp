using Microsoft.AspNetCore.Identity;

namespace MusicBookingApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Role { get; set; }
        public required bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
