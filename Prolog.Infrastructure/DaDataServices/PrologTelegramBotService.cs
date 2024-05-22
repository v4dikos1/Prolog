using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Prolog.Abstractions.Enums;
using Prolog.Abstractions.Services;
using Prolog.Application.Orders;
using Prolog.Domain;
using Prolog.Domain.Entities;
using Prolog.Domain.Enums;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Prolog.Infrastructure.DaDataServices;

internal class PrologTelegramBotService: IPrologBotService, IDisposable
{
    private readonly IConfiguration _configuration;
    private TelegramBotClient? _botClient;
    private readonly ApplicationDbContext _dbContext;
    private readonly IOrderMapper _orderMapper;

    public PrologTelegramBotService(IConfiguration configuration, ApplicationDbContext dbContext, IOrderMapper orderMapper)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _orderMapper = orderMapper;
        GetBot().Wait();
    }

    private async Task<TelegramBotClient> GetBot()
    {
        if (_botClient != null)
        {
            return _botClient;
        }

        var hostUrl = _configuration["TelegramBotService:HostUrl"];
        var accessToken = _configuration["TelegramBotService:AccessToken"];

        if (string.IsNullOrEmpty(hostUrl) || string.IsNullOrEmpty(accessToken))
        {
            throw new InvalidOperationException("HostUrl or AccessToken for TelegramBotService is not configured properly.");
        }

        _botClient = new TelegramBotClient(accessToken);
        try
        {
            await _botClient.SetWebhookAsync(hostUrl);
        }
        catch (Exception ex)
        {
            
        }
        
        return _botClient;
    }

    public void Dispose()
    {
        _botClient?.CloseAsync();
    }

    public async Task ViewStartMenu(Update requestModel, CancellationToken cancellationToken)
    {
        var inlineKeyboard = new ReplyKeyboardMarkup(new[]
        {
            new[]
            {
                new KeyboardButton(BotCommands.OrdersList),
                new KeyboardButton(BotCommands.Profile)
            }
        }) { ResizeKeyboard = true };

        var client = await GetBot();
        await client.SendTextMessageAsync(requestModel.Message!.Chat.Id,
            "Добро пожаловать в Prolog.Bot! Здесь вы сможете просмотреть доступные вам заявки на доставку!",
            parseMode: ParseMode.Markdown, replyMarkup: inlineKeyboard, cancellationToken: cancellationToken);
    }

    public async Task SendOrdersAsync(Update requestModel, CancellationToken cancellationToken)
    {
        var client = await GetBot();

        var orders = await _dbContext.Orders
            .AsNoTracking()
            .Include(x => x.DriverTransportBind)
            .ThenInclude(b => b!.Driver)
            .Include(x => x.Customer)
            .Include(x => x.Storage)
            .Where(x => !x.IsArchive)
            .Where(x => x.OrderStatus == OrderStatusEnum.Planned)
            .Where(x => x.DriverTransportBind!.Driver.Telegram == requestModel.Message!.Chat.Username)
            .OrderBy(x => x.DeliveryDateFrom)
            .ToListAsync(cancellationToken);

        var ordersModel = orders
            .Select(_orderMapper.MapToBotViewModel).ToList();

        if (ordersModel.Any())
        {
            var messageBuilder = new StringBuilder();
            var inlineKeyboardButtons = new List<InlineKeyboardButton>();

            foreach (var order in ordersModel)
            {
                var pickUpStartTime = order.PickUpStartDate.ToOffset(TimeSpan.FromHours(7)).ToString("HH:mm");
                var pickUpEndTime = order.PickUpEndDate.ToOffset(TimeSpan.FromHours(7)).ToString("HH:mm");
                var deliveryStartTime = order.DeliveryStartDate?.ToOffset(TimeSpan.FromHours(7)).ToString("HH:mm");
                var deliveryEndTime = order.DeliveryEndDate?.ToOffset(TimeSpan.FromHours(7)).ToString("HH:mm");

                messageBuilder.AppendLine($"*Заказ {order.VisibleId}*");
                messageBuilder.AppendLine($"*Клиент:* {order.ClientName}");
                messageBuilder.AppendLine($"*Телефон:* {order.ClientPhone}");
                messageBuilder.AppendLine($"*Адрес:* {order.Address}");
                messageBuilder.AppendLine($"*Склад:* {order.StorageName}");
                messageBuilder.AppendLine($"*Адрес склада:* {order.StorageAddress}");
                messageBuilder.AppendLine($"*Время забора:* {pickUpStartTime} - {pickUpEndTime}");
                if (deliveryStartTime != null && deliveryEndTime != null)
                {
                    messageBuilder.AppendLine($"*Время доставки:* {deliveryStartTime} - {deliveryEndTime}");
                }
                messageBuilder.AppendLine();

                inlineKeyboardButtons.Add(InlineKeyboardButton.WithCallbackData($"Заказ {order.VisibleId} доставлен", $"{order.Id}"));
            }

            var tableButtons = new List<InlineKeyboardButton[]>();
            for (var i = 0; i < inlineKeyboardButtons.Count; i += 2)
            {
                var row = inlineKeyboardButtons.Skip(i).Take(1).ToArray();
                tableButtons.Add(row);
            }

            var inlineKeyboard = new InlineKeyboardMarkup(tableButtons.ToArray());
            await client.SendTextMessageAsync(requestModel.Message!.Chat.Id,
                messageBuilder.ToString(),
                parseMode: ParseMode.Markdown,
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }
        else
        {
            await client.SendTextMessageAsync(requestModel.Message!.Chat.Id,
                "Активных заявок на данный момент нет!", cancellationToken: cancellationToken);
        }
    }

    public async Task GetProfileAsync(Update requestModel, CancellationToken cancellationToken)
    {
        var client = await GetBot();

        var driver = await _dbContext.Drivers
            .Where(x => x.Telegram == requestModel.Message!.Chat.Username)
            .SingleOrDefaultAsync(cancellationToken);

        if (driver == null)
        {
            await client.SendTextMessageAsync(requestModel.Message!.Chat.Id,
                "Вы не зарегистрированы в качестве водителя в системе Prolog! Обратитесь к администратору системы для уточнения вопросов.",
                cancellationToken: cancellationToken);
            return;
        }

        var messageBuilder = new StringBuilder();
        messageBuilder.AppendLine($"{driver.Name} {driver.Surname} {driver.Patronymic}");
        messageBuilder.AppendLine($"*Номер телефона:* {driver.PhoneNumber}");
        messageBuilder.AppendLine($"*Оклад:* {driver.Salary}");
        await client.SendTextMessageAsync(requestModel.Message!.Chat.Id,
            messageBuilder.ToString(),
            parseMode: ParseMode.Markdown,
            cancellationToken: cancellationToken);
    }


    public async Task SendMessageAsync(Update requestModel, string message, CancellationToken cancellationToken)
    {
        var client = await GetBot();
        await client.SendTextMessageAsync(requestModel.Message!.Chat.Id,
            message, cancellationToken: cancellationToken);
    }
}