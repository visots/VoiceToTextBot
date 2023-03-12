using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceToTextBot.Configuration
{
    public class AppSettings
    {
        /// <summary>
        /// Telegram Bot токен
        /// </summary>
        public string BotToken { get; set; }
        /// <summary>
        /// Папка загрузки аудио файлов
        /// </summary>
        public string DownloadsFolder { get; set; }
        /// <summary>
        /// Имя файла при загрузке
        /// </summary>
        public string AudioFileName { get; set; }
        /// <summary>
        /// Формат аудио при загрузке
        /// </summary>
        public string InputAudioFormat { get; set; }

        /// <summary>
        /// Целевой формат аудио
        /// </summary>
        public string OutputAudioFormat { get; set; }

        /// <summary>
        /// Битрейт
        /// </summary>
        public string InputAudioBitrate { get; set; }
    }
}
