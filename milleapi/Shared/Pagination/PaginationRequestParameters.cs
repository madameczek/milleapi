using System.ComponentModel.DataAnnotations;

namespace milleapi.Shared.Pagination;

public class PaginationRequestParameters
{
    /// <summary>
    /// Optional, [Range(1, int.MaxValue)] 
    /// </summary>
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }

    /// <summary>
    /// Optional, [Range(1, 100)], default = 25
    /// </summary>
    [Range(1, 100)] 
    public int PageSize { get; set; } = 25;
}