using CommunityToolkit.Mvvm.ComponentModel;
using Material.Icons;
using Meowlicious.Services.Localization;
using Meowlicious.ViewModels;
using System;

namespace Meowlicious.Services.Layout
{
    public partial class LayoutService : ObservableObject, ILayoutService
    {
        public ILocalizationService L { get; }

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
        [NotifyPropertyChangedFor(nameof(HeaderText))]
        private Type? _currentPageType;
        public bool IsExplorerActive => CurrentPageType == typeof(RecipeExplorerViewModel);
        public bool IsFavoritesActive => CurrentPageType == typeof(FavoritesViewModel);
        public bool IsAddRecipeActive => CurrentPageType == typeof(AddRecipeViewModel);
        public bool IsSettingsActive => CurrentPageType == typeof(SettingsViewModel);
        public bool IsAboutActive => CurrentPageType == typeof(AboutViewModel);
        public bool IsOpenedRecipeActive => CurrentPageType == typeof(OpenedRecipeViewModel);

        public MaterialIconKind ExplorerIcon => IsExplorerActive ? MaterialIconKind.Home : MaterialIconKind.HomeOutline;
        public MaterialIconKind FavoritesIcon => IsFavoritesActive ? MaterialIconKind.Heart : MaterialIconKind.HeartOutline;
        public MaterialIconKind AddIcon => IsAddRecipeActive ? MaterialIconKind.Pencil : MaterialIconKind.PencilOutline;
        public MaterialIconKind SettingsIcon => IsSettingsActive ? MaterialIconKind.Cog : MaterialIconKind.CogOutline;
        public MaterialIconKind AboutIcon => IsAboutActive ? MaterialIconKind.Information : MaterialIconKind.InformationOutline;

        [ObservableProperty]
        private string? _headerText;

        public LayoutService(ILocalizationService localizationService)
        {
            L = localizationService;

            L.PropertyChanged += (_, __) =>
            {
                if (!IsOpenedRecipeActive)
                    HeaderText = GetDefaultHeaderText();
            };
        }

        partial void OnCurrentPageTypeChanged(Type? value)
        {
            if (!IsOpenedRecipeActive)
            {
                HeaderText = GetDefaultHeaderText();
            }
        }
        private string? GetDefaultHeaderText()
        {
            return IsExplorerActive ? L["ExplorerHeader"] :
                   IsFavoritesActive ? L["FavoritesHeader"] :
                   IsAddRecipeActive ? L["AddRecipeHeader"] :
                   IsSettingsActive ? L["SettingsHeader"] :
                   IsAboutActive ? L["AboutHeader"] :
                   string.Empty;
        }
    }
}

