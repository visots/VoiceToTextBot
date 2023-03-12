using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace VoiceToTextBot.Controllers
{
    internal class DefaultMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public DefaultMessageController(ITelegramBotClient telegramBotClient)
        {
            this._telegramBotClient = telegramBotClient;
        }
    }
}
