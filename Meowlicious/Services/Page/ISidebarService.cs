using Material.Icons;
using System;

namespace Meowlicious.Services.Page
{
    public interface ISidebarService
    {
        Type? CurrentPageType { get; set; }

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
