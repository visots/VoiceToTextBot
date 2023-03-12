using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoiceToTextBot.Services;

namespace VoiceToTextBot.Controllers
{
    internal class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramBotClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            this._telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            Console.WriteLine("Контроллер " + GetType().Name);

            if (callbackQuery?.Data == null)
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;

            string languageText = callbackQuery.Data switch
            {
                "ru" => "Русский",
                "en" => "Английский",
                _ => String.Empty
            };

            string msg = $"<b>Язык аудио - {languageText}.{Environment.NewLine}</b>" +
                            $"{Environment.NewLine}Можно поменять в главном меню.";

            await _telegramBotClient.SendTextMessageAsync(callbackQuery.From.Id,msg, cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
