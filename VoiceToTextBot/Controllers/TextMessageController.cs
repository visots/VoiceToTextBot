using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace VoiceToTextBot.Controllers
{
    internal class TextMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            this._telegramBotClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            string msg = string.Empty;
            switch (message.Text)
            {
                case "/start":
                    var buttons = CreateButtons();

                    msg = $"<b>  Наш бот превращает аудио в текст.</b> {Environment.NewLine}" +
                       $"{Environment.NewLine}Можно записать сообщение и переслать другу, если лень печатать.{Environment.NewLine}";

                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,msg, cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                
                default:
                    msg = "Отправьте аудио для превращения в текст.";
                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,msg,cancellationToken:ct);
                    break;
            }
        }

        private List<InlineKeyboardButton[]> CreateButtons()
        {
            var buttons = new List<InlineKeyboardButton[]>();
            buttons.Add(new[]
            {
                        InlineKeyboardButton.WithCallbackData($" Русский", $"ru"),
                        InlineKeyboardButton.WithCallbackData($" Английский", "en")

                    });
            return buttons;
        }
    }
}
