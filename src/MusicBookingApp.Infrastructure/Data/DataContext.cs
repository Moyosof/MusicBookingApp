using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MusicBookingApp.Application.Utility;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Domain.Enums;

namespace MusicBookingApp.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // we don't want ef core SQL logs.
                optionsBuilder.UseLoggerFactory(
                    LoggerFactory.Create(builder =>
                    {
                        builder.AddFilter(
                            (category, level) =>
                                category == DbLoggerCategory.Database.Command.Name
                                && level == LogLevel.Error
                        );
                    })
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // this convert dateTime property to UTC in our db entities
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var dateTimeProperties = entityType
                    .GetProperties()
                    .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?));

                foreach (var property in dateTimeProperties)
                {
                    property.SetValueConverter(new DateTimeToUtcConverter());
                }
            }
            modelBuilder
                .Entity<Booking>()
                .Property(x => x.Status)
                .HasConversion(o => o.ToFriendlyString(), o => o.ToBookingStatus());
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Artist> Artist { get; set; } = null!;
    }
}