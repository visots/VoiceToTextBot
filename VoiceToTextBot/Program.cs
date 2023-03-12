using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using VoiceToTextBot.Controllers;

namespace VoiceToTextBot
{
    internal class Program
    {
        private static string _botToken = "5817286574:AAFwdZUxJN4ZQP2MZJ_aqmvCiecTOktW_yk";

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
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<VoiceMessageController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<ITelegramBotClient>(privider => new TelegramBotClient(_botToken));
            
            services.AddHostedService<Bot>();
        }
    }
}