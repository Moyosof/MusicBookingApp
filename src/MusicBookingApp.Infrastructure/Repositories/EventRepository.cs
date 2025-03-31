using Microsoft.EntityFrameworkCore;

using MusicBookingApp.Application.Repositories;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Infrastructure.Data;
using MusicBookingApp.Infrastructure.Repositories.Base;

namespace MusicBookingApp.Infrastructure.Repositories
{
    public class EventRepository(DataContext context) : BaseRepository<Event>(context), IEventRepository
    {
        public async Task<bool> EventNameExistsAsync(string artistId, string eventName)
        {
            return await Context.Events.AnyAsync(x =>
                x.ArtistId == artistId
                && x.Name == eventName
            );
        }
    }
}
