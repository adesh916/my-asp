using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly string apiKey = "9df826d4e50d2c840afb92d5e09b7a16"; // Replace with your OpenWeatherMap API key

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                ViewBag.Error = "Please enter a location.";
                return View();
            }

            WeatherViewModel weather = await GetWeatherAsync(location);
            return View(weather);
        }

        private async Task<WeatherViewModel> GetWeatherAsync(string location)
        {
            WeatherViewModel weather = new WeatherViewModel();

            string url = $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={apiKey}&units=metric";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(result);

                    weather.Location = json["name"].ToString();
                    weather.Description = json["weather"][0]["description"].ToString();
                    weather.Temperature = float.Parse(json["main"]["temp"].ToString());
                    weather.Humidity = float.Parse(json["main"]["humidity"].ToString());
                    weather.WindSpeed = float.Parse(json["wind"]["speed"].ToString());
                }
                else
                {
                    ViewBag.Error = "Could not retrieve data. Please try again.";
                }
            }

            return weather;
        }
    }
}