using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoiceToTextBot.Controllers;

namespace VoiceToTextBot
{
    internal class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramBotClient;

        private DefaultMessageController _defaultMessageController;
        private TextMessageController _textMessageController;
        private VoiceMessageController _voiceMessageController;
        private InlineKeyboardController _inlineKeyboardController;

        public Bot(ITelegramBotClient telegramBotClient,
                    DefaultMessageController messageController,
                    TextMessageController textMessageController,
                    VoiceMessageController voiceMessageController,
                    InlineKeyboardController keyboardController)
        {
            _telegramBotClient = telegramBotClient;
            _defaultMessageController = messageController;
            _textMessageController = textMessageController;
            _voiceMessageController = voiceMessageController;
            _inlineKeyboardController = keyboardController;
        }

        /// <summary>
        /// Обработка действий пользователя
        /// </summary>
        /// <param name="client"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _inlineKeyboardController.Handle(update.CallbackQuery,cancellationToken);
                return;
            }

            if(update.Type == UpdateType.Message)
            {
                switch(update.Message!.Type)
                {
                    case MessageType.Text:
                        await _textMessageController.Handle(update.Message, cancellationToken);
                        break;
                    case MessageType.Voice:
                        await _voiceMessageController.Handle(update.Message, cancellationToken);
                        break;
                    default:
                        await _defaultMessageController.Handle(update.Message, cancellationToken);
                        break;
                }
            }
        }

        /// <summary>
        /// Обработка ошибки
        /// </summary>
        /// <param name="client"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);

            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramBotClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync,
                new ReceiverOptions() { AllowedUpdates = { } }, cancellationToken: stoppingToken);
            Console.WriteLine("Бот запущен");
            return Task.CompletedTask;
        }
    }
}
