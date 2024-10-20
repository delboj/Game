namespace SharedKernel;

/// <summary>
/// Paginated list
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedList<T> : List<T>
{
    public PagedList(List<T> items, int page, int pageSize, int totalCount)
        : base(items)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
}
