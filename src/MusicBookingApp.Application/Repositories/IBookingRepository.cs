using MusicBookingApp.Application.Repositories.Base;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<int> GetCurrentAttendeesCountAsync(string eventId, CancellationToken cancellationToken);
        Task<Booking?> GetExistingBookingByEmailAsync(string eventId, string email, CancellationToken cancellationToken);
    }
}
