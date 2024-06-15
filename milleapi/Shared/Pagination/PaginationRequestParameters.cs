using System.ComponentModel.DataAnnotations;

namespace milleapi.Shared.Pagination;

public class PaginationRequestParameters
{
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }

    [Range(1, 100)] 
    public int PageSize { get; set; } = 25;
}