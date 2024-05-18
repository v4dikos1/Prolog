using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prolog.Abstractions.Enums;
using Prolog.Abstractions.Services;
using Prolog.Application.Orders.Commands;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Prolog.Api.Controllers;

[AllowAnonymous]
[Route("api/message/update")]
public class TelegramBotController(IPrologBotService prologBotService, ISender sender,
    ILogger<TelegramBotController> logger): BaseController
{
    [HttpPost]
    public async Task Update([FromBody] object request, CancellationToken cancellationToken)
    {
        logger.LogError(request.ToString());
        var updateRequest = JsonConvert.DeserializeObject<Update>(request.ToString() ?? string.Empty);
        if (updateRequest != null)
        {
            await Execute(updateRequest, cancellationToken);
        }
    }

    private async Task Execute(Update update, CancellationToken cancellationToken)
    {
        if (update.Message?.Chat == null && update.CallbackQuery == null)
        {
            return;
        }

        if (update.Type == UpdateType.Message)
        {
            switch (update.Message?.Text)
            {
                case BotCommands.Start:
                    await prologBotService.ViewStartMenu(update, cancellationToken);
                    return;

                case BotCommands.OrdersList:
                    await prologBotService.SendOrdersAsync(update, cancellationToken);
                    return;
                case BotCommands.Profile:
                    await prologBotService.SendMessageAsync(update, "Профиль", cancellationToken);
                    return;
            }
        }

        if (update.Type == UpdateType.CallbackQuery)
        {
            var completeOrderCommand = new CompleteOrderCommand
            {
                OrderId = Convert.ToInt64(update.CallbackQuery!.Data)
            };
            await sender.Send(completeOrderCommand, cancellationToken);
        }
    }

}