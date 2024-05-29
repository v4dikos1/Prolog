namespace Prolog.Application.Hubs;

public interface IPrologHub
{
    /// <summary>
    /// Заявки запланированы
    /// </summary>
    /// <returns></returns>
    Task OrdersPlanned();
}