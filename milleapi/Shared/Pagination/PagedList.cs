using Microsoft.EntityFrameworkCore;

namespace milleapi.Shared.Pagination;

public interface IPagedList
{
    int CurrentPage { get; }
    int TotalPages { get; }
    int PageSize { get; }
    int TotalCount { get; }
    bool HasPrevious { get; }
    bool HasNext { get; }
}

public class PagedList<T> : List<T>, IPagedList where T : class
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasPrevious => (CurrentPage > 1);
    public bool HasNext => (CurrentPage < TotalPages);

    private PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        AddRange(items);
    }

    public static async Task<PagedList<T>> Create(
        IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default)
    {
        var count = source.Count();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: ct);
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public static async Task<PagedList<T>> Create(
        IQueryable<T> source,
        int pageNumber,
        int pageSize,
        Func<T> transformFunc,
        CancellationToken ct = default)
    {
        var count = source.Count();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken: ct);
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
