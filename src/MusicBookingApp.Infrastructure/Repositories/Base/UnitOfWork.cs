using MusicBookingApp.Application.Repositories;
using MusicBookingApp.Application.Repositories.Base;
using MusicBookingApp.Infrastructure.Data;

namespace MusicBookingApp.Infrastructure.Repositories.Base
{
    public class UnitOfWork(
        DataContext context
    ) : IUnitOfWork
    {
        public IBookingRepository Bookings => new BookingRepository(context);

        public IArtistRepository Artists => new ArtistRepository(context);

        public IEventRepository Events => new EventRepository(context);

        public int Complete()
        {
            return context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}
