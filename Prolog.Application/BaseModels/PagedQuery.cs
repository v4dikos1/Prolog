﻿using Microsoft.AspNetCore.Mvc;
using Prolog.Core.EntityFramework.Features.SearchPagination.Interfaces;

namespace Prolog.Application.BaseModels;

public class PagedQuery : IPagedQuery
{
    /// <summary>
    /// Пагинация - сколько необходимо получить элементов.
    /// </summary>
    [FromQuery]
    public int? Limit { get; set; }

    /// <summary>
    /// Пагинация - сколько необходимо пропустить элементов.
    /// </summary>
    [FromQuery]
    public int? Offset { get; set; }
}