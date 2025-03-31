using MediatR;
using Microsoft.AspNetCore.Authorization;
using MusicBookingApp.Application.Extensions;
using Microsoft.AspNetCore.Mvc;
using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Application.Contracts;
using MusicBookingApp.Application.Features.User.Queries.GetArtistById;
using MusicBookingApp.Host.Controllers.Base;
using MusicBookingApp.Application.Utility;
using MusicBookingApp.Application.Features.User.Command.UpdateArtistProfile;

namespace MusicBookingApp.Host.Controllers
{
    [Authorize]
    public class UserController(IMediator mediator, ICurrentUser currentUser) : BaseController
    {
        #region ARTIST

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<GetArtistByUserIdResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById()
        {
            var result = await mediator.Send(new GetArtistByUserIdRequest { UserId = currentUser.UserId });
            return result.Match(_ => Ok(result.ToSuccessfulApiResponse()), ReturnErrorResponse);
        }

        [HttpPost("update-artist-profile")]
        [ProducesResponseType(typeof(ApiResponse<MyUnit>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CompleteUserProfile(string userId, UpdateArtistProfileRequestDto requestDto)
        {
            var result = await mediator.Send(new UpdateArtistProfileRequest
            {
                UserId = currentUser.UserId,
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Bio = requestDto.Bio,
                Genre = requestDto.Genre,
                PhoneNumber = requestDto.PhoneNumber,
                StageName = requestDto.StageName
            });
            return result.Match(_ => Ok(result.ToSuccessfulApiResponse()), ReturnErrorResponse);
        }
        #endregion
    }
}