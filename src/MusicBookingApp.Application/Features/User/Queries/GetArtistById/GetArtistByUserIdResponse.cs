namespace MusicBookingApp.Application.Features.User.Queries.GetArtistById
{
    public class GetArtistByUserIdResponse
    {
        public required string ArtistId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string PhoneNumber { get; set; }
        public required string StageName { get; set; }
        public required string Genre { get; set; }
        public required string Bio { get; set; }
        public required bool IsActive { get; set; }
    }
}