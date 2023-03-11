using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;

namespace StringLenghtCounterBot
{
    internal class Program
    {
        private static string _botToken = "6150513213:AAHaxTeA3sxYCgE_vDT1Wo_on66vbkEqagQ";

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
            services.AddSingleton<ITelegramBotClient>(privider => new TelegramBotClient(_botToken));

            services.AddHostedService<Bot>();
        }
    }
}