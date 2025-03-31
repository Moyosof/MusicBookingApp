using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Application.Contracts;
using MusicBookingApp.Application.Extensions;
using MusicBookingApp.Application.Features.Events.Command.CreateEvent;
using MusicBookingApp.Application.Features.Events.Command.UpdateEvent;
using MusicBookingApp.Application.Features.Events.Queries;
using MusicBookingApp.Application.Features.Events.Queries.GetEventById;
using MusicBookingApp.Application.Features.Events.Queries.GetEvents;
using MusicBookingApp.Application.Utility;
using MusicBookingApp.Host.Controllers.Base;

namespace MusicBookingApp.Host.Controllers
{
    public class EventController(IMediator mediator, ICurrentUser currentUser) : BaseController
    {
        #region EVENT

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreateEventResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateEventAsync(
            [FromBody] CreateEventRequestDto requestDto
        )
        {
            var result = await mediator.Send(
                new CreateEventRequest
                {
                    UserId = currentUser.UserId,
                    EventDate = requestDto.EventDate,
                    EventName = requestDto.EventName,
                    Location = requestDto.Location,
                    MaxAttendees = requestDto.MaxAttendees,
                    TicketPrice = requestDto.TicketPrice,
                }
            );
            return result.Match(_ => Ok(result.ToSuccessfulApiResponse()), ReturnErrorResponse);
        }

        [HttpPatch("{eventId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<MyUnit>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEventAsync(
            string eventId,
            [FromBody] UpdateEventRequestDto requestDto
        )
        {
            var result = await mediator.Send(
                new UpdateEventRequest
                {
                    UserId = currentUser.UserId,
                    EventId = eventId,
                    EventDate = requestDto.EventDate,
                    EventName = requestDto.EventName,
                    Location = requestDto.Location,
                    MaxAttendees = requestDto.MaxAttendees,
                    TicketPrice = requestDto.TicketPrice,
                }
            );
            return result.Match(_ => Ok(result.ToSuccessfulApiResponse()), ReturnErrorResponse);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<GetEventResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEventByIdAsync(
            [FromQuery] GetEventByIdRequestDto requestDto
        )
        {
            var result = await mediator.Send(
                new GetEventByIdRequest
                {
                    UserId = currentUser.UserId,
                    EventId = requestDto.EventId,
                }
            );
            return result.Match(_ => Ok(result.ToSuccessfulApiResponse()), ReturnErrorResponse);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(ApiResponse<GetEventResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEventsRequestAsync(
            [FromQuery] GetEventsRequestDto requestDto
        )
        {
            var result = await mediator.Send(new GetEventsRequest { UserId = currentUser.UserId });
            return result.Match(_ => Ok(result.ToSuccessfulApiResponse()), ReturnErrorResponse);
        }
        #endregion
    }
}
