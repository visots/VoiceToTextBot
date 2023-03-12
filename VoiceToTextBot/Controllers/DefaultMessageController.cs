using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace VoiceToTextBot.Controllers
{
    internal class DefaultMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public DefaultMessageController(ITelegramBotClient telegramBotClient)
        {
            this._telegramBotClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name}");
            await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Получено сообщение не поддерживаемого формата", cancellationToken: ct);
        }
    }
}
