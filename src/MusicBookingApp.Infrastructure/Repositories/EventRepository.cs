using MusicBookingApp.Application.Repositories;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Infrastructure.Data;
using MusicBookingApp.Infrastructure.Repositories.Base;

namespace MusicBookingApp.Infrastructure.Repositories
{
    public class EventRepository(DataContext context) : BaseRepository<Event>(context), IEventRepository
    {
    }
}
