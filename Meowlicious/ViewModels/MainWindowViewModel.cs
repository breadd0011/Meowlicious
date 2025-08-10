using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Meowlicious.Services.Layout;
using Meowlicious.Services.Localization;
using Meowlicious.Services.Navigation;
using Meowlicious.Services.Search;
using Meowlicious.Utils;
using System;
using System.Threading.Tasks;

namespace Meowlicious.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty] private INavigationService _navService;
        [ObservableProperty] private ISearchService _searchService;
        [ObservableProperty] private ILayoutService _layoutService;

        [ObservableProperty] private bool _isUpdateAvailable = false;
        [ObservableProperty] private string _headerText = string.Empty;
        public ILocalizationService L { get; }

        public MainWindowViewModel(
            INavigationService navService,
            ILocalizationService localizationService,
            ISearchService searchService,
            ILayoutService layoutService)
        {
            _navService = navService;
            L = localizationService;
            _searchService = searchService;
            _layoutService = layoutService;

            GoToExplorer();

            if (AutoUpdater.UpdateManager.IsInstalled)
            {
                CheckForUpdates();
            }

        }

        private void CheckForUpdates()
        {
            Task.Run(async () =>
            {
                try
                {
                    bool IsUpdateDownloaded = false;

                    await AutoUpdater.CheckForUpdatesAsync();

                    if (AutoUpdater.UpdateAvailable)
                    {
                        await AutoUpdater.DownloadUpdateAsync();
                        IsUpdateDownloaded = true;
                    }

                    Dispatcher.UIThread.Post(() =>
                    {
                        if (IsUpdateDownloaded)
                        {
                            IsUpdateAvailable = true;
                        }
                    });
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }

        [RelayCommand]
        private void GoToExplorer()
        {
            NavService.NavigateTo<RecipeExplorerViewModel>();
            LayoutService.CurrentPageType = typeof(RecipeExplorerViewModel);
        }

        [RelayCommand]
        private void GoToFavorites()
        {
            NavService.NavigateTo<FavoritesViewModel>();
            LayoutService.CurrentPageType = typeof(FavoritesViewModel);
        }

        [RelayCommand]
        private void GoToSettings()
        {
            NavService.NavigateTo<SettingsViewModel>();
            LayoutService.CurrentPageType = typeof(SettingsViewModel);
        }

        [RelayCommand]
        private void GoToAbout()
        {
            NavService.NavigateTo<AboutViewModel>();
            LayoutService.CurrentPageType = typeof(AboutViewModel);
        }

        [RelayCommand]
        private void AddRecipe()
        {
            NavService.NavigateTo<AddRecipeViewModel>();
            LayoutService.CurrentPageType = typeof(AddRecipeViewModel);
        }

        [RelayCommand]
        private void UpdateApp()
        {
            AutoUpdater.UpdateAndRestartApp();
        }
    }
}
