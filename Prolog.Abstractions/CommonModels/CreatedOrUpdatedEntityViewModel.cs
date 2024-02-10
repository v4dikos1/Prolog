namespace Prolog.Abstractions.CommonModels;

public class CreatedOrUpdatedEntityViewModel<T>(T id)
{
    /// <summary>
    /// Идентификатор добавленной сущности
    /// </summary>
    public T Id { get; set; } = id;
}

public class CreatedOrUpdatedEntityViewModel(Guid id) : CreatedOrUpdatedEntityViewModel<Guid>(id);