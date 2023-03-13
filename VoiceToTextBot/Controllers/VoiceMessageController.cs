using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceToTextBot.Configuration;
using VoiceToTextBot.Services;
using VoiceToTextBot.Models;

namespace VoiceToTextBot.Controllers
{
    internal class VoiceMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly AppSettings _appSettings;
        private readonly IFileHandler _fileHandler;
        private readonly IStorage _storage;

        public VoiceMessageController(ITelegramBotClient telegramBotClient, AppSettings appSettings, IFileHandler fileHandler, IStorage storage)
        {
            this._telegramBotClient = telegramBotClient;
            _appSettings = appSettings;
            _fileHandler = fileHandler;
            _storage = storage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name}");

            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;

            await _fileHandler.Download(fileId, ct);

            //await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, "Голосовое сообщение загружено", cancellationToken: ct);

            Console.WriteLine("Файл сохранен "+ fileId);

            string languageCode = _storage.GetSession(message.Chat.Id).LanguageCode;
            var result =  _fileHandler.Process(languageCode);

            if (string.IsNullOrEmpty(result))
                result = "Не удалось распознать";

            await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, result, cancellationToken: ct);
        }
    }
}
