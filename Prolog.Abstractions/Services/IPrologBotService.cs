using Telegram.Bot.Types;

namespace Prolog.Abstractions.Services;

public interface IPrologBotService
{
    Task ViewStartMenu(Update requestModel, CancellationToken cancellationToken);
    Task SendOrdersAsync(Update requestModel, CancellationToken cancellationToken);
    Task GetProfileAsync(Update requestModel, CancellationToken cancellationToken);
    Task SendMessageAsync(Update requestModel, string message, CancellationToken cancellationToken);
}