
using Microsoft.EntityFrameworkCore;

using MusicBookingApp.Application.Repositories;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Infrastructure.Data;
using MusicBookingApp.Infrastructure.Repositories.Base;

namespace MusicBookingApp.Infrastructure.Repositories
{
    public class ArtistRepository(DataContext context) : BaseRepository<Artist>(context), IArtistRepository
    {
        public async Task<Artist?> GetArtistByUserIdAsNoTrackingAsync(string userId, CancellationToken cancellationToken)
        {
            return await Context.Artist.AsNoTracking().Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        }

        public async Task<Artist?> GetArtistByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await Context.Artist.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        }
    }
}
