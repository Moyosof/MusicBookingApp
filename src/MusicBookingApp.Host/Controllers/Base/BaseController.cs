using System.Net.Mime;
using System.Runtime.InteropServices;

using Microsoft.AspNetCore.Mvc;

using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Domain.Enums;
using MusicBookingApp.Domain.ServiceErrors;
using MusicBookingApp.Infrastructure.Filters;

namespace MusicBookingApp.Host.Controllers.Base
{
    [ApiController]
    [TypeFilter(typeof(CustomValidationFilter))]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    public abstract class BaseController : ControllerBase
    {
        protected static IActionResult ReturnErrorResponse(List<Error> errors)
        {
            string message = "One or more errors occurred.";
            if (errors.All((Error e) => e.Type == ErrorType.Validation))
            {
                message = "One or more validation errors occurred.";
            }

            if (errors.Any((Error e) => e.Type == ErrorType.Unexpected))
            {
                message = "Something went wrong.";
            }

            Error error = errors[0];
            int value = error.Type switch
            {
                ErrorType.NotFound => 404,
                ErrorType.Validation => 400,
                ErrorType.Conflict => 409,
                ErrorType.Failure => 400,
                ErrorType.Unauthorized => 401,
                _ => 500,
            };
            List<ApiError> errors2 = errors.Select((Error x) => new ApiError
            {
                Code = x.Code,
                Description = x.Description
            }).ToList();
            ApiErrorResponse value2 = new ApiErrorResponse(errors2, message);
            return new ObjectResult(value2)
            {
                StatusCode = value
            };
        }

        protected static IActionResult ReturnErrorResponse(Error error)
        {
            string message = error.Type switch
            {
                ErrorType.Validation => "A validation error occurred.",
                ErrorType.Unexpected => "Something went wrong.",
                _ => "An error occurred.",
            };
            int value = error.Type switch
            {
                ErrorType.NotFound => 404,
                ErrorType.Validation => 400,
                ErrorType.Conflict => 409,
                ErrorType.Failure => 400,
                ErrorType.Unauthorized => 401,
                _ => 500,
            };
            ApiError apiError = new ApiError
            {
                Code = error.Code,
                Description = error.Description
            };
            List<ApiError> list = new List<ApiError>();
            CollectionsMarshal.SetCount(list, 1);
            Span<ApiError> span = CollectionsMarshal.AsSpan(list);
            int num = 0;
            span[num] = apiError;
            num++;
            ApiErrorResponse value2 = new ApiErrorResponse(list, message);
            return new ObjectResult(value2)
            {
                StatusCode = value
            };
        }
    }
}