using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace VoiceToTextBot.Controllers
{
    internal class VoiceMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public VoiceMessageController(ITelegramBotClient telegramBotClient)
        {
            this._telegramBotClient = telegramBotClient;
        }
    }
}
