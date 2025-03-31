namespace MusicBookingApp.Application.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository Bookings { get; }
        IArtistRepository Artists { get; }
        IEventRepository Events { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}
