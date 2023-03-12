using Telegram.Bot;
using Telegram.Bot.Types;

namespace VoiceToTextBot.Controllers
{
    internal class VoiceMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public VoiceMessageController(ITelegramBotClient telegramBotClient)
        {
            this._telegramBotClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name}");
            await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Получено голосовое сообщение", cancellationToken: ct);
        }
    }
}
