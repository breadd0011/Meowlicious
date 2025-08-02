using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Meowlicious.Services.Navigation;
using Meowlicious.ViewModels;

namespace Meowlicious.Services
{
    public partial class NavigationService : ViewModelBase, INavigationService
    {
        [ObservableProperty] private ViewModelBase _currentView;

        private Func<Type, ViewModelBase> _viewModelFactory;

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModelBase>() where TViewModelBase : ViewModelBase
        {
            ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModelBase));
            CurrentView = viewModel;
        }

        public void NavigateTo(ViewModelBase viewModel)
        {
            CurrentView = viewModel;
        }
    }
}
