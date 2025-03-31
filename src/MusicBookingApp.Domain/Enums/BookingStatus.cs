namespace MusicBookingApp.Domain.Enums
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Canceled
    }
    public static class BookingStatusExtensions
    {
        public static BookingStatus ToBookingStatus(this string status)
        {
            return status.ToLower() switch
            {
                "pending" => BookingStatus.Pending,
                "confirmed" => BookingStatus.Confirmed,
                "canceled" => BookingStatus.Canceled,
                _ => throw new NotImplementedException($"Unknown booking status type: {status}")
            };
        }

        public static string ToFriendlyString(this BookingStatus bookingStatus)
        {
            return bookingStatus.ToString();
        }
    }
}
