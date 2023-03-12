﻿using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Bot(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
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
                await _telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, "Нажата кнопка", cancellationToken: cancellationToken);
                return;
            }

            if (update.Type == UpdateType.Message)
            {
                Console.WriteLine($"Получено сообщение: {update.Message.Text}");
                await _telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, "Сообщение отправлено", cancellationToken: cancellationToken);
                return;
            }
        }

        /// <summary>
        /// Обработка ошибки
        /// </summary>
        /// <param name="client"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task HandleErrorAsync (ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}", _ => exception.ToString()
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
