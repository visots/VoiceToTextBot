using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoiceToTextBot.Services;

namespace StringLenghtCounterBot.Controllers
{
    internal class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).ActionCode = callbackQuery.Data;

            string actionCode = callbackQuery.Data switch
            {
                "cnt" => "Количество симолов",
                "sum" => "Сумма чисел",
                _ => String.Empty
            };

            string msg = $"<b> Выбрано действие - {actionCode}.{Environment.NewLine}</b>" +
                            $"{Environment.NewLine}Можно поменять в главном меню.";

            await _telegramBotClient.SendTextMessageAsync(callbackQuery.From.Id, msg, cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
