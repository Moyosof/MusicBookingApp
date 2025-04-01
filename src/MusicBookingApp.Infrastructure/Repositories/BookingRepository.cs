using MediatR;
using System.Threading;

using MusicBookingApp.Application.Repositories;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Infrastructure.Data;
using MusicBookingApp.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace MusicBookingApp.Infrastructure.Repositories
{
    public class BookingRepository(DataContext context) : BaseRepository<Booking>(context), IBookingRepository
    {
        public async Task<Booking?> GetExistingBookingByEmailAsync(string eventId, string email, CancellationToken cancellationToken)
        {
            return await Context.Bookings
                .AsNoTracking()
                .Where(b => b.EventId == eventId && b.BookieEmail == email)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> GetCurrentAttendeesCountAsync(string eventId, CancellationToken cancellationToken)
        {
            return await Context.Bookings
                .AsNoTracking()
                .Where(b => b.EventId == eventId)
                .CountAsync(cancellationToken);
        }

    }
}
