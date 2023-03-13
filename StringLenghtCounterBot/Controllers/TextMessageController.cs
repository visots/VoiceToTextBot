using StringLenghtCounterBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using VoiceToTextBot.Services;

namespace StringLenghtCounterBot.Controllers
{
    internal class TextMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private IStringProcessor _stringProcessor;
        private IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStringProcessor stringProcessor, IStorage memoryStorage)
        {
            this._telegramBotClient = telegramBotClient;
            _stringProcessor = stringProcessor;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            string msg = string.Empty;
            switch (message.Text)
            {
                case "/start":
                    var buttons = CreateButtons();

                    msg = $"<b>  Выбери действие в меню:</b> {Environment.NewLine}";

                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, msg, cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    
                    string actionCode = _memoryStorage.GetSession(message.Chat.Id).ActionCode;

                    msg = _stringProcessor.ProcessString(actionCode,message.Text);
                    
                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, msg, cancellationToken: ct);

                    break;
            }
        }

        private List<InlineKeyboardButton[]> CreateButtons()
        {
            var buttons = new List<InlineKeyboardButton[]>();
            buttons.Add(new[]
            {
                        InlineKeyboardButton.WithCallbackData($"Подсчет суммы", $"sum"),
                        InlineKeyboardButton.WithCallbackData($"Подсчет количества символов", "cnt")

                    });
            return buttons;
        }
    }
}
