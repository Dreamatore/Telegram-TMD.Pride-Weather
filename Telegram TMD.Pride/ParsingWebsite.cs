using Newtonsoft.Json;
namespace Telegram_TMD.Pride
{
    class ParsingWebsite
    {
        public static WeatherResponse? ReadWeather(string namecity)
        {
            
            string URL = $"https://api.openweathermap.org/data/2.5/weather?q={namecity}&appid={ApplicationSettings.APIKeyWeather}&lang=ru&units=metric";

            string responseBody = ReadJson(URL);

            var weather_reponse = JsonConvert.DeserializeObject<WeatherResponse>(responseBody);

            return weather_reponse;
        }

        public static string ReadJson(string URL)
        {
            using HttpClient httpClient = new HttpClient();

            HttpRequestMessage _request = new HttpRequestMessage(HttpMethod.Get, URL);

            var _response = httpClient.Send(_request);

            using var reader = new StreamReader(_response.Content.ReadAsStream());

            return reader.ReadToEnd();
        }
    }
}