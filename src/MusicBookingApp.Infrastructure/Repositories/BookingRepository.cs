using MusicBookingApp.Application.Repositories;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Infrastructure.Data;
using MusicBookingApp.Infrastructure.Repositories.Base;

namespace MusicBookingApp.Infrastructure.Repositories
{
    public class BookingRepository(DataContext context) : BaseRepository<Booking>(context), IBookingRepository
    {
        //public async Task<Otp?> GetOtpByUserId(string userId)
        //{
        //    return await Context.Otps.FirstOrDefaultAsync(o => o.UserId == userId);
        //}
    }
}
