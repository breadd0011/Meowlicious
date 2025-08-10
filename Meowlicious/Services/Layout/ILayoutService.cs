using Material.Icons;
using System;

namespace Meowlicious.Services.Layout
{
    public interface ILayoutService
    {
        Type? CurrentPageType { get; set; }
        string? HeaderText { get; set; }
        bool IsExplorerActive { get; }
        bool IsFavoritesActive { get; }
        bool IsAddRecipeActive { get; }
        bool IsSettingsActive { get; }
        bool IsAboutActive { get; }

        MaterialIconKind ExplorerIcon { get; }
        MaterialIconKind FavoritesIcon { get; }
        MaterialIconKind AddIcon { get; }
        MaterialIconKind SettingsIcon { get; }
        MaterialIconKind AboutIcon { get; }
    }
}
