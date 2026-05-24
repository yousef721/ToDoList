using TodoList.Shared.Constants;

namespace TodoList.Shared.Helpers;

public sealed class TodoQueryParameters
{
    private int _pageNumber = ApplicationConstants.DefaultPageNumber;
    private int _pageSize = ApplicationConstants.DefaultPageSize;

    public string? SearchTerm { get; set; }
    // public string Status { get; set; } = "All";
    public string UserId { get; set; } = string.Empty;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? ApplicationConstants.DefaultPageNumber : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value is < 1 or > ApplicationConstants.MaxPageSize
            ? ApplicationConstants.DefaultPageSize
            : value;
    }
}
