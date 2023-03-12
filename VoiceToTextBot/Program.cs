using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using VoiceToTextBot.Controllers;
using VoiceToTextBot.Services;
using VoiceToTextBot.Configuration;

namespace VoiceToTextBot
{
    internal class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build();

            Console.WriteLine("Сервис запущен");
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuilAppSettings();
            services.AddSingleton(BuilAppSettings());

            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<VoiceMessageController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddSingleton<IFileHandler, AudioFileHandler>();

            services.AddSingleton<ITelegramBotClient>(privider => new TelegramBotClient(appSettings.BotToken));

            services.AddHostedService<Bot>();
        }

        static AppSettings BuilAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "5817286574:AAFwdZUxJN4ZQP2MZJ_aqmvCiecTOktW_yk",
                DownloadsFolder = "C:\\Users\\visot\\Downloads",
                AudioFileName = "audio",
                InputAudioFormat = "ogg",
                OutputAudioFormat = "wav"
            };
        }
    }
}