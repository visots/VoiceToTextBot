using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace VoiceToTextBot.Controllers
{
    internal class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient)
        {
            this._telegramBotClient = telegramBotClient;
        }
    }
}
