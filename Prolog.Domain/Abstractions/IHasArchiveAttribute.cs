namespace Prolog.Domain.Abstractions;

public interface IHasArchiveAttribute
{
    /// <summary>
    /// Статус архивности
    /// </summary>
    public bool IsArchive { get; set; }
}