using MusicBookingApp.Domain.Constants;

namespace MusicBookingApp.Application.ApiResponses;

public abstract class PaginationParameters
{
    public int PageNumber { get; set; } = SearchConstants.PAGE_NUMBER;
    private int _pageSize = SearchConstants.MIN_PAGE_SIZE;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > SearchConstants.MAX_PAGE_SIZE ? SearchConstants.MAX_PAGE_SIZE : value;
    }
}