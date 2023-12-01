using System;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels;

public class SecondPageViewModel : PageViewModelBase
{
    public ReactiveCommand<Unit, Unit> LoadDataCommand { get; }

    private Root _weatherData = new();
    public Root WeatherData
    {
        get => _weatherData;
        set => this.RaiseAndSetIfChanged(ref _weatherData, value);
    }
    
    public SecondPageViewModel()
    {
        LoadDataCommand = ReactiveCommand.CreateFromTask(InitializeAsync);
        LoadDataCommand.Execute().Subscribe();
    }

    // 异步初始化方法
    private async Task InitializeAsync()
    {
        try
        {
            var data = await new ApiServices(Locator.Current.GetService<HttpClient>() ?? throw new InvalidOperationException())
                .GetWeatherByCityAsync("Taichung");

            if (data is null)
            {
                // TODO: Show error message
                return;
            }

            WeatherData = data;
            Console.WriteLine(WeatherData.List[0].Weather[0].IconImage);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading weather data: " + ex.Message);
        }
    }

    public override bool CanNavigateNext
    {
        get => false;
        protected set => throw new NotSupportedException();
    }

    public override bool CanNavigateBack
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
}