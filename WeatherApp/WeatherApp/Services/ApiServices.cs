using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class ApiServices
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "e9bead3ce6662fb07b5f377564f90b93";
    private const string ApiUrl = "https://api.openweathermap.org/data/2.5/forecast";
    
    public ApiServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<Root?> GetWeatherByCityAsync(string city)
    {
        var url = $"{ApiUrl}?q={city}&appid={ApiKey}";
        Console.WriteLine(url);
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var weatherData = JsonConvert.DeserializeObject<Root>(content);
            return weatherData;
        }

        throw new Exception("Unable to fetch weather data.");
    }
}