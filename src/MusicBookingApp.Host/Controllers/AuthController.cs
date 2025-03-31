using MediatR;

using Microsoft.AspNetCore.Mvc;
using MusicBookingApp.Application.Extensions;
using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Application.Features.Auth;
using MusicBookingApp.Application.Features.Auth.Command.Login;
using MusicBookingApp.Application.Features.Auth.Command.Register;
using MusicBookingApp.Host.Controllers.Base;

namespace MusicBookingApp.Host.Controllers
{
    public class AuthController(IMediator mediator) : BaseController
    {
        #region SIGNUP AND LOGIN

        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<UserAuthResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var result = await mediator.Send(request);
            return result.Match(_ => Ok(result.ToSuccessfulApiResponse()), ReturnErrorResponse);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<UserAuthResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var result = await mediator.Send(request);
            return result.Match(_ => Ok(result.ToSuccessfulApiResponse()), ReturnErrorResponse);
        }

        #endregion
    }
}