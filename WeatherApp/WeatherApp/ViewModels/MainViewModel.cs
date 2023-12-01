using System.Reactive;
using DynamicData;
using ReactiveUI;

namespace WeatherApp.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        _pages = new PageViewModelBase[] {
            new FirstPageViewModel(this),
            new SecondPageViewModel()
        };

        _currentPage = _pages[0];

        var canNavNext = this.WhenAnyValue(x => x.CurrentPage.CanNavigateNext);
        var canNavBack = this.WhenAnyValue(x => x.CurrentPage.CanNavigateBack);

        NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
        NavigateBackCommand = ReactiveCommand.Create(NavigateBack, canNavBack);
    }

    private readonly PageViewModelBase[] _pages;

    private PageViewModelBase _currentPage;

    public PageViewModelBase CurrentPage
    {
        get => _currentPage;
        private set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }
    
    public ReactiveCommand<Unit, Unit> NavigateNextCommand { get; }
    public ReactiveCommand<Unit, Unit> NavigateBackCommand { get; }

    private void NavigateNext()
    {
        var index = _pages.IndexOf(CurrentPage) + 1;
        CurrentPage = _pages[index];
    }
    
    private void NavigateBack()
    {
        var index = _pages.IndexOf(CurrentPage) - 1;
        CurrentPage = _pages[index];
    }
}