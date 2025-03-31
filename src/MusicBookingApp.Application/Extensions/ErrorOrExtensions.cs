
using FluentValidation.Results;
using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Domain.ServiceErrors;

namespace MusicBookingApp.Application.Extensions;

public static class ErrorOrExtensions
{
    public static ApiResponse<T> ToSuccessfulApiResponse<T>(this Result<T> result)
    {
        return new ApiResponse<T>(result.Value, "Success", success: true);
    }

    public static List<Error> ToErrorList(this ValidationResult validationResult)
    {
        return validationResult.Errors.Select((ValidationFailure x) => Error.Validation(x.ErrorCode, x.ErrorMessage)).ToList();
    }

    public static TResult Match<T, TResult>(this Result<T> result, Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        if (result == null)
        {
            throw new ArgumentNullException("result");
        }

        if (!result.IsSuccess)
        {
            return onFailure(result.Error);
        }

        return onSuccess(result.Value);
    }
}