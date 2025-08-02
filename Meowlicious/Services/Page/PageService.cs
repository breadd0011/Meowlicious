using CommunityToolkit.Mvvm.ComponentModel;
using RecipeApp.ViewModels;
using System;

namespace RecipeApp.Services.Page
{
    public partial class PageService : ObservableObject, IPageService
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsExplorerActive))]
        [NotifyPropertyChangedFor(nameof(IsFavoritesActive))]
        [NotifyPropertyChangedFor(nameof(IsAddRecipeActive))]
        [NotifyPropertyChangedFor(nameof(IsSettingsActive))]
        [NotifyPropertyChangedFor(nameof(IsAboutActive))]
        private Type? _currentPageType;

        public bool IsExplorerActive => CurrentPageType == typeof(RecipeExplorerViewModel);
        public bool IsFavoritesActive => CurrentPageType == typeof(FavoritesViewModel);
        public bool IsAddRecipeActive => CurrentPageType == typeof(AddRecipeViewModel);
        public bool IsSettingsActive => CurrentPageType == typeof(SettingsViewModel);
        public bool IsAboutActive => CurrentPageType == typeof(AboutViewModel);
    }
}
