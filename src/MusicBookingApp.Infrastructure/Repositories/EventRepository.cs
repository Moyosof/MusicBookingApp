using Microsoft.EntityFrameworkCore;
using MusicBookingApp.Application.Repositories;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Infrastructure.Data;
using MusicBookingApp.Infrastructure.Repositories.Base;

namespace MusicBookingApp.Infrastructure.Repositories
{
    public class EventRepository(DataContext context)
        : BaseRepository<Event>(context),
            IEventRepository
    {
        public async Task<bool> EventNameExistsAsync(string artistId, string eventName)
        {
            return await Context.Events.AnyAsync(x =>
                x.ArtistId == artistId && x.Name == eventName
            );
        }

        public async Task<Event?> GetEventByIdAsNoTrackingAsync(string eventId)
        {
            return await Context
                .Events.AsNoTracking()
                .Include(x => x.Bookings)
                .Include(x => x.Artist)
                .ThenInclude(x => x.User)
                .Where(x => x.Id == eventId)
                .FirstOrDefaultAsync();
        }
    }
}
