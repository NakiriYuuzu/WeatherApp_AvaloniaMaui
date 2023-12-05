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
    public ReactiveCommand<Unit, Unit> OnSearchButtonClicked { get; }
    public ReactiveCommand<Unit, Unit> LoadDataCommand { get; }
    private ReactiveCommand<Unit, Unit> OnSearchDataCommand { get; set; }
    
    private string _searchText;
    public string SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }
    
    private bool _isTextBoxVisible;
    public bool IsTextBoxVisible
    {
        get => _isTextBoxVisible;
        set => this.RaiseAndSetIfChanged(ref _isTextBoxVisible, value);
    }

    private Root _weatherData = new();
    public Root WeatherData
    {
        get => _weatherData;
        set => this.RaiseAndSetIfChanged(ref _weatherData, value);
    }
    
    public SecondPageViewModel()
    {
        IsTextBoxVisible = false;
        OnSearchButtonClicked = ReactiveCommand.Create(OnSearchClicked);
        LoadDataCommand = ReactiveCommand.CreateFromTask(() => InitializeAsync("Taichung"));
        LoadDataCommand.Execute().Subscribe();
    }

    // 异步初始化方法
    private async Task InitializeAsync(string city)
    {
        try
        {
            var data = await new ApiServices(Locator.Current.GetService<HttpClient>() ?? throw new InvalidOperationException())
                .GetWeatherByCityAsync(city);

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

    private void OnSearchClicked()
    {
        if (!IsTextBoxVisible)
        {
            IsTextBoxVisible = true;
        }
        else
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                IsTextBoxVisible = false;
                OnSearchDataCommand = ReactiveCommand.CreateFromTask(() => InitializeAsync(SearchText));
                OnSearchDataCommand.Execute().Subscribe();
            }
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