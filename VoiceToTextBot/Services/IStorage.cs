using VoiceToTextBot.Models;

namespace VoiceToTextBot.Services
{
    public interface IStorage
    {
        /// <summary>
        /// Получение сессии
        /// </summary>
        /// <param name="chatId">ИД чата</param>
        /// <returns>Объект сессии</returns>
        Session GetSession(long chatId);
    }
}
