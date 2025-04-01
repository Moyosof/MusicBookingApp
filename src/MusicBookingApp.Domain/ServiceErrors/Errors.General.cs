namespace MusicBookingApp.Domain.ServiceErrors
{
    public static partial class Errors
    {
        public static class General
        {
            public static Error EventNameExist =>
                Error.NotFound(
                    code: "General.EventNameExist",
                    description: "Event already exist, please enter a different name."
                );

            public static Error EventNotFound =>
                Error.NotFound(code: "General.EventNotFound", description: "Event not found");

            public static Error EventFullyBooked =>
                Error.Failure(
                    code: "General.EventFullyBooked",
                    description: "Oops 🤦‍♂️ This event is fully booked"
                );

            public static Error AlreadyBooked =>
               Error.Failure(
                   code: "General.AlreadyBooked",
                   description: "You have already booked this event.👌"
               );
        }
    }
}
