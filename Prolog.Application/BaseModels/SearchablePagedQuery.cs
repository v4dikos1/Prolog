using Microsoft.AspNetCore.Mvc;
using Prolog.Core.EntityFramework.Features.SearchPagination.Interfaces;

namespace Prolog.Application.BaseModels;

public class SearchablePagedQuery : PagedQuery, ISearchQuery
{
    /// <summary>
    /// Строка поиска.
    /// </summary>
    [FromQuery]
    public string? SearchQuery { get; set; }
}
