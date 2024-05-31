
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Linq;

namespace Telegram_TMD.Pride
{
    class TelegramBot
    {
        private static TelegramBotClient client = new TelegramBotClient(ApplicationSettings.APIKeyTelegram);

        public void WorkBot()
        {
            client.StartReceiving(
                updateHandler: OnMessageHandler,
                pollingErrorHandler: HandlePollingErrorAsync);
            Console.ReadKey();

        }


        async Task OnMessageHandler(object sender, Update update, CancellationToken cancellationToken)
        {
            var msg = update.Message;

            try
            {
                if (msg.Text != null || msg.Text != "")
                {

                    if (msg.Text == "/start" || msg.Text == "Привет" || msg.Text == "здарова")
                    {
                        await client.SendTextMessageAsync(msg.Chat.Id, "Привет! Я телеграмм бот TMD.Pride от SPD.Riot, который выполнит любое твоё пожелание относительно погоды. Чтобы начать, укажите название нужного города!");
                    }
                    else if (msg.Text == "/d")
                    {
                        var city = DataBase.ReadCity(msg.Chat.Id);
                        if (city == "")
                        {
                            await client.SendTextMessageAsync(msg.Chat.Id, "Город не установлен, либо ещё не сохранён");
                        }
                        var weather = ParsingWebsite.ReadWeather(city);

                        if (weather == null) return;
                        var res = string.Join(",", weather.Weather.Select(x => x.Description).FirstOrDefault(), weather.Main.FeelsLike);

                        await client.SendTextMessageAsync(msg.Chat.Id, res);
                    }
                    else
                    {



                        DataBase.UpdateCity(msg.Chat.Id, msg.Text);

                        var weather = ParsingWebsite.ReadWeather(msg.Text);

                        if (weather == null) return;
                        var res = string.Join(",", weather.Weather.Select(x => x.Description).FirstOrDefault(), weather.Main.FeelsLike);

                        await client.SendTextMessageAsync(msg.Chat.Id, res);


                    }
                }


            }
            catch { Console.WriteLine("TMD.Pride ERROR | Произошла ошибка! (или закончились API calls) | " + msg.Text); }


        }



        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
