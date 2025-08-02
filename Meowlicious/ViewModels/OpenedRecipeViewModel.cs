using CommunityToolkit.Mvvm.ComponentModel;
using RecipeApp.Models;
using RecipeApp.Services.Localization;

namespace RecipeApp.ViewModels
{
    public partial class OpenedRecipeViewModel : ViewModelBase
    {
        [ObservableProperty] private Recipe _currentRecipe;
        public ILocalizationService L { get; }
        public OpenedRecipeViewModel(
            Recipe recipe,
            ILocalizationService localizationService)
        {
            _currentRecipe = recipe;
            L = localizationService;
        }

    }
}
