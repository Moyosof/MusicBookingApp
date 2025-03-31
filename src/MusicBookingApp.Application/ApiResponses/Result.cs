using MusicBookingApp.Domain.ServiceErrors;

namespace MusicBookingApp.Application.ApiResponses
{
    public class Result<T>
    {
        private readonly T _value;

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public T Value
        {
            get
            {
                if (!IsSuccess)
                {
                    return default(T);
                }

                return _value;
            }
        }

        public Result(bool isSuccess, T value, Error error)
        {
            if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
            {
                throw new ArgumentException("Invalid error", "error");
            }

            IsSuccess = isSuccess;
            _value = (T)(isSuccess ? ((object)value) : ((object)default(T)));
            Error = error;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(isSuccess: true, value, Error.None);
        }

        public static Result<T> Failure(Error error)
        {
            return new Result<T>(isSuccess: false, default(T), error);
        }

        public static Result<T> Failure(List<Error> errors)
        {
            if (errors.Count == 0)
            {
                throw new ArgumentException("Error list cannot be empty.", "errors");
            }

            Error error = errors.First();
            return new Result<T>(isSuccess: false, default(T), error);
        }
    }
}
