using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicBookingApp.Application.ApiResponses;
using MusicBookingApp.Application.Extensions;
using MusicBookingApp.Application.Repositories.Base;

namespace MusicBookingApp.Application.Features.Events.Queries.GetEvents
{
    public class GetEventsRequestDto : PaginationParameters { }

    public class GetEventsRequest
        : PaginationParameters,
            IRequest<Result<PagedResponse<GetEventResponse>>>
    {
        public string UserId { get; set; } = null!;
    }

    public class Handler(
        IUnitOfWork unitOfWork,
        ILogger<Handler> logger,
        IValidator<GetEventsRequest> validator
    ) : IRequestHandler<GetEventsRequest, Result<PagedResponse<GetEventResponse>>>
    {
        public async Task<Result<PagedResponse<GetEventResponse>>> Handle(
            GetEventsRequest request,
            CancellationToken cancellationToken
        )
        {
            logger.LogInformation(
                "Attempting to get all events request for user with Id: {UserId}",
                request.UserId
            );
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ToErrorList();
                logger.LogError(
                    "Validation errors occurred while get all events. Errors: {errors}",
                    errors
                );

                return Result<PagedResponse<GetEventResponse>>.Failure(errors);
            }

            var query = unitOfWork
                .Events.GetQueryable()
                .AsNoTracking()
                .Include(x => x.Artist)
                .ThenInclude(x => x.User);

            var responseQuery = query
                .OrderByDescending(x => x.DateCreated)
                .Select(x => EventMapper.ToGetEventResponse(x));

            var response = await new PagedResponse<GetEventResponse>().ToPagedList(
                responseQuery,
                request.PageNumber,
                request.PageSize
            );
            logger.LogInformation(
                "Successfully retrieved all events for user: {userId}. Total Count: {totalCount}.",
                request.UserId,
                response.TotalCount
            );
            return Result<PagedResponse<GetEventResponse>>.Success(response);
        }
    }

    public class Validation : AbstractValidator<GetEventsRequest>
    {
        public Validation()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
