using System;

namespace Meowlicious.Services.Page
{
    public interface IPageService
    {
        Type? CurrentPageType { get; set; }

        bool IsExplorerActive { get; }
        bool IsFavoritesActive { get; }
        bool IsAddRecipeActive { get; }
        bool IsSettingsActive { get; }
        bool IsAboutActive { get; }
    }
}
