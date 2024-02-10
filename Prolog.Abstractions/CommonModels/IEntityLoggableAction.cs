namespace Prolog.Abstractions.CommonModels;

/// <summary>
/// Действие(команда, метод), которое необходимо залогировать в таблицу ActionLog.
/// </summary>
public interface IEntityLoggableAction : ILoggableAction
{
    /// <summary>
    /// Получение идентификатора сущности, по которой будут фильтроваться логи
    /// </summary>
    Guid GetEntityId();
}

/// <summary>
/// Действие(команда, метод), которое необходимо залогировать в таблицу ActionLog
/// </summary>
public interface ILoggableAction { }