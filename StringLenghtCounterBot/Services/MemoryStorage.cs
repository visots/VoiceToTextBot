using System.Collections.Concurrent;
using VoiceToTextBot.Models;

namespace VoiceToTextBot.Services
{
    internal class MemoryStorage : IStorage
    {
        /// <summary>
        /// Хранилище сессий
        /// </summary>
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            var newSession = new Session() { ActionCode = "ru"};
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
