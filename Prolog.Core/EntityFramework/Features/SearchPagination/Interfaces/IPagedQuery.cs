﻿namespace Prolog.Core.EntityFramework.Features.SearchPagination.Interfaces;

public interface IPagedQuery
{
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
