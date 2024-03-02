using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Prolog.Core.Utils;
using System.Linq.Expressions;
using System.Text.Json;

namespace Prolog.Core.Loggers.Helpers;

public static class LoggableExtensions
{
    public static IQueryable<T> WhereJsonFieldEquals<T>(
        this IQueryable<T> query,
        Expression<Func<T, JsonDocument>> jsonDocumentExpression,
        string propertyName,
        string value)
    {
        Defend.Against.Null(query, nameof(query));
        Defend.Against.Null(jsonDocumentExpression, nameof(jsonDocumentExpression));
        Defend.Against.NullOrEmpty(propertyName, nameof(propertyName));
        Defend.Against.InvalidMemberAccessChain(jsonDocumentExpression, out var jsonDocumentMemberAccessExpression,
            nameof(jsonDocumentExpression));

        var jsonPropertyName = jsonDocumentMemberAccessExpression.Member.Name;
        return query.Where(x =>
            EF.Property<JsonDocument>(x!, jsonPropertyName).RootElement.GetProperty(propertyName).GetString() ==
            value);
    }

    public static IQueryable<T> WhereJsonFieldIn<T>(
        this IQueryable<T> query,
        Expression<Func<T, JsonDocument>> jsonDocumentExpression,
        string propertyName,
        IEnumerable<string> values)
    {
        Defend.Against.Null(query, nameof(query));
        Defend.Against.Null(jsonDocumentExpression, nameof(jsonDocumentExpression));
        Defend.Against.NullOrEmpty(propertyName, nameof(propertyName));
        var valuesList = values.ToList();
        Defend.Against.Null(valuesList, nameof(values));
        Defend.Against.InvalidMemberAccessChain(jsonDocumentExpression, out var jsonDocumentMemberAccessExpression, nameof(jsonDocumentExpression));

        var jsonPropertyName = jsonDocumentMemberAccessExpression.Member.Name;
        return query.Where(x =>
            valuesList.Contains(EF.Property<JsonDocument>(x!, jsonPropertyName).RootElement.GetProperty(propertyName).GetString()!));
    }
}