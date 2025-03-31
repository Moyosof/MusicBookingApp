
using MusicBookingApp.Application.Repositories;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Infrastructure.Data;
using MusicBookingApp.Infrastructure.Repositories.Base;

namespace MusicBookingApp.Infrastructure.Repositories
{
    public class ArtistRepository(DataContext context) : BaseRepository<Artist>(context), IArtistRepository
    {
    }
}
