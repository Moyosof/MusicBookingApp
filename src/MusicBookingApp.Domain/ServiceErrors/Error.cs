using MusicBookingApp.Domain.Enums;
using System.Runtime.CompilerServices;

namespace MusicBookingApp.Domain.ServiceErrors
{
    public sealed record Error(string Code, string Description, ErrorType Type)
    {
        public static readonly Error None = new Error(string.Empty, string.Empty, ErrorType.Unexpected);

        public static Error Validation(string code, string description)
        {
            return new Error(code, description, ErrorType.Validation);
        }

        public static Error NotFound(string code, string description)
        {
            return new Error(code, description, ErrorType.NotFound);
        }

        public static Error Failure(string code, string description)
        {
            return new Error(code, description, ErrorType.Failure);
        }

        public static Error ServerError(string code, string description)
        {
            return new Error(code, description, ErrorType.ServerError);
        }

        public static Error Unauthorized(string code, string description)
        {
            return new Error(code, description, ErrorType.Unauthorized);
        }

        [CompilerGenerated]
        private Error(Error original)
        {
            Code = original.Code;
            Description = original.Description;
            Type = original.Type;
        }
    }
}
