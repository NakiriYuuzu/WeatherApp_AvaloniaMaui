using System;
using System.Reactive;
using ReactiveUI;

namespace WeatherApp.ViewModels;

public class FirstPageViewModel: PageViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    
    public string Title => "This is First Page, Going to Second Page.";
    public ReactiveCommand<Unit, Unit> NavigateNextCommand { get; }
    
    public FirstPageViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        NavigateNextCommand = ReactiveCommand.Create(NavigateNext);
    }
    
    private void NavigateNext()
    {
        _mainViewModel.NavigateNextCommand.Execute().Subscribe();
    }
    
    public override bool CanNavigateNext
    {
        get => true;
        protected set => throw new NotSupportedException();
    }

    public override bool CanNavigateBack
    {
        get => false;
        protected set => throw new NotSupportedException();
    }
}