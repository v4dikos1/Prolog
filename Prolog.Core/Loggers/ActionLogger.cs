using Ardalis.GuardClauses;
using Prolog.Core.Utils;

namespace Prolog.Core.Loggers;

public abstract class ActionLogger
{
    private readonly Dictionary<string, object?> _logData = new();

    /// <summary>
    /// Добавление фильтр-записи в лог-запись
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="value">значение</param>
    public void LogData(string key, object? value)
    {
        Defend.Against.NullOrEmpty(key, nameof(key));
        var result = _logData.TryAdd(key, value);
        if (!result)
        {
            throw new ArgumentException($"Запись с ключом {key} уже добавлена в фильтры логов!");
        }
    }

    /// <summary>
    /// Получение фильтр-записей из лог-записи
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, object?> GetLogData()
    {
        return _logData;
    }
}