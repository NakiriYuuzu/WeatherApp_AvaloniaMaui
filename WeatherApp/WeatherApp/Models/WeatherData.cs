using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WeatherApp.Services;
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace WeatherApp.Models
{
    public class City
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("coord")] public Coord Coord { get; set; }

        [JsonProperty("country")] public string Country { get; set; }

        [JsonProperty("population")] public int Population { get; set; }

        [JsonProperty("timezone")] public int Timezone { get; set; }

        [JsonProperty("sunrise")] public int Sunrise { get; set; }

        [JsonProperty("sunset")] public int Sunset { get; set; }
    }

    public class Clouds
    {
        [JsonProperty("all")] public int All { get; set; }
    }

    public class Coord
    {
        [JsonProperty("lat")] public double Lat { get; set; }

        [JsonProperty("lon")] public double Lon { get; set; }
    }

    public class List
    {
        [JsonProperty("dt")] public int Dt { get; set; }

        [JsonProperty("main")] public Main Main { get; set; }

        [JsonProperty("weather")] public List<Weather> Weather { get; set; }

        [JsonProperty("clouds")] public Clouds Clouds { get; set; }

        [JsonProperty("wind")] public Wind Wind { get; set; }

        [JsonProperty("visibility")] public int Visibility { get; set; }

        [JsonProperty("pop")] public double Pop { get; set; }

        [JsonProperty("rain")] public Rain Rain { get; set; }

        [JsonProperty("sys")] public Sys Sys { get; set; }

        private string _dtTxt;
        [JsonProperty("dt_txt")] public string DtTxt { get => _dtTxt.Substring(5, 11); set => _dtTxt = value; }
    }

    public class Main
    {
        private double _temp;
        [JsonProperty("temp")] public double Temp { get => Math.Round(_temp - 273.15, 0); set => _temp = value; }

        [JsonProperty("feels_like")] public double FeelsLike { get; set; }

        [JsonProperty("temp_min")] public double TempMin { get; set; }

        [JsonProperty("temp_max")] public double TempMax { get; set; }

        [JsonProperty("pressure")] public int Pressure { get; set; }

        [JsonProperty("sea_level")] public int SeaLevel { get; set; }

        [JsonProperty("grnd_level")] public int GrndLevel { get; set; }

        [JsonProperty("humidity")] public int Humidity { get; set; }

        [JsonProperty("temp_kf")] public double TempKf { get; set; }
    }

    public class Rain
    {
        [JsonProperty("3h")] public double _3h { get; set; }
    }

    public class Root
    {
        [JsonProperty("cod")] public string Cod { get; set; }

        [JsonProperty("message")] public int Message { get; set; }

        [JsonProperty("cnt")] public int Cnt { get; set; }

        [JsonProperty("list")] public List<List> List { get; set; }

        [JsonProperty("city")] public City City { get; set; }
    }

    public class Sys
    {
        [JsonProperty("pod")] public string Pod { get; set; }
    }

    public class Weather
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("main")] public string Main { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        private string _icon;
        [JsonProperty("icon")] public string Icon { get => $"https://openweathermap.org/img/wn/{_icon}@2x.png"; set => _icon = value; }
        public Bitmap IconImage { get => ImageHelper.LoadFromResource(new Uri($"avares://WeatherApp/Assets/icon_{_icon}.png")); }
    }

    public class Wind
    {
        [JsonProperty("speed")] public double Speed { get; set; }

        [JsonProperty("deg")] public int Deg { get; set; }

        [JsonProperty("gust")] public double Gust { get; set; }
    }
}