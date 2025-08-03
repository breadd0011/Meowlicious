using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Meowlicious.Services.Localization;
using Meowlicious.Services.Navigation;
using Meowlicious.Services.Page;
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
        [ObservableProperty] private ISidebarService _SidebarService;

        [ObservableProperty] private bool _isUpdateAvailable = false;
        public ILocalizationService L { get; }

        public MainWindowViewModel(
            INavigationService navService,
            ILocalizationService localizationService,
            ISearchService searchService,
            ISidebarService SidebarService)
        {
            _navService = navService;
            L = localizationService;
            _searchService = searchService;
            _SidebarService = SidebarService;

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
            SidebarService.CurrentPageType = typeof(RecipeExplorerViewModel);
        }

        [RelayCommand]
        private void GoToFavorites()
        {
            NavService.NavigateTo<FavoritesViewModel>();
            SidebarService.CurrentPageType = typeof(FavoritesViewModel);
        }

        [RelayCommand]
        private void GoToSettings()
        {
            NavService.NavigateTo<SettingsViewModel>();
            SidebarService.CurrentPageType = typeof(SettingsViewModel);
        }

        [RelayCommand]
        private void GoToAbout()
        {
            NavService.NavigateTo<AboutViewModel>();
            SidebarService.CurrentPageType = typeof(AboutViewModel);
        }

        [RelayCommand]
        private void AddRecipe()
        {
            NavService.NavigateTo<AddRecipeViewModel>();
            SidebarService.CurrentPageType = typeof(AddRecipeViewModel);
        }

        [RelayCommand]
        private void UpdateApp()
        {
            AutoUpdater.UpdateAndRestartApp();
        }
    }
}
