namespace MusicBookingApp.Domain.ServiceErrors
{
    public static partial class Errors
    {

        public static class General
        {
            public static Error EventNameExist => Error.NotFound(
                code: "General.EventNameExist",
                description: "Event already exist, please enter a different name.");
        }
    }
}
