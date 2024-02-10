namespace Prolog.Domain.Entities;

public abstract class BaseEntity<T>
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public T Id { get; set; } = default!;
}