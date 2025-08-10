using CommunityToolkit.Mvvm.ComponentModel;
using Material.Icons;
using Meowlicious.ViewModels;
using System;

namespace Meowlicious.Services.Page
{
    public partial class SidebarService : ObservableObject, ISidebarService
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsExplorerActive))]
        [NotifyPropertyChangedFor(nameof(IsFavoritesActive))]
        [NotifyPropertyChangedFor(nameof(IsAddRecipeActive))]
        [NotifyPropertyChangedFor(nameof(IsSettingsActive))]
        [NotifyPropertyChangedFor(nameof(IsAboutActive))]
        [NotifyPropertyChangedFor(nameof(ExplorerIcon))]
        [NotifyPropertyChangedFor(nameof(FavoritesIcon))]
        [NotifyPropertyChangedFor(nameof(AddIcon))]
        [NotifyPropertyChangedFor(nameof(SettingsIcon))]
        [NotifyPropertyChangedFor(nameof(AboutIcon))]
        private Type? _currentPageType;

        public bool IsExplorerActive => CurrentPageType == typeof(RecipeExplorerViewModel);
        public bool IsFavoritesActive => CurrentPageType == typeof(FavoritesViewModel);
        public bool IsAddRecipeActive => CurrentPageType == typeof(AddRecipeViewModel);
        public bool IsSettingsActive => CurrentPageType == typeof(SettingsViewModel);
        public bool IsAboutActive => CurrentPageType == typeof(AboutViewModel);

        public MaterialIconKind ExplorerIcon => IsExplorerActive ? MaterialIconKind.Home : MaterialIconKind.HomeOutline;

        public MaterialIconKind FavoritesIcon => IsFavoritesActive ? MaterialIconKind.Heart : MaterialIconKind.HeartOutline;

        public MaterialIconKind AddIcon => IsAddRecipeActive ? MaterialIconKind.Pencil : MaterialIconKind.PencilOutline;

        public MaterialIconKind SettingsIcon => IsSettingsActive ? MaterialIconKind.Cog : MaterialIconKind.CogOutline;

        public MaterialIconKind AboutIcon => IsAboutActive ? MaterialIconKind.Information : MaterialIconKind.InformationOutline;
    }
}
