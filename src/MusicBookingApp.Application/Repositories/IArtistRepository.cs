using MusicBookingApp.Application.Repositories.Base;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Repositories
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<Artist?> GetArtistByUserIdAsNoTrackingAsync(string userId, CancellationToken cancellationToken);
        Task<Artist?> GetArtistByUserIdAsync(string userId, CancellationToken cancellationToken);
    }
}
