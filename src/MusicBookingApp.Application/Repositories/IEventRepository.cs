using MusicBookingApp.Application.Repositories.Base;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<bool> EventNameExistsAsync(string artistId, string eventName);
        Task<Event?> GetEventByIdAsNoTrackingAsync(string eventId);
    }
}
