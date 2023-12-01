namespace WeatherApp.ViewModels;

public abstract class PageViewModelBase : ViewModelBase
{
    public abstract bool CanNavigateNext { get; protected set; }
    public abstract bool CanNavigateBack { get; protected set; }
}