using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Meowlicious.Models;
using Meowlicious.Services;
using Meowlicious.Services.Localization;
using Meowlicious.Services.Navigation;

namespace Meowlicious.ViewModels
{
    public partial class FavoritesViewModel : ViewModelBase
    {
        [ObservableProperty] private INavigationService _navService;
        [ObservableProperty] private ObservableCollection<Recipe> _recipes = new();

        private readonly IRecipeDataService _recipeDataService;
        public ILocalizationService L { get; }
        public FavoritesViewModel(
            INavigationService navService,
            IRecipeDataService recipeDataService,
            ILocalizationService localizationService)
        {
            _navService = navService;
            _recipeDataService = recipeDataService;
            L = localizationService;

            Recipes = _recipeDataService.GetFavoriteRecipes();
        }

        [RelayCommand]
        private void OpenRecipe(Recipe recipe)
        {
            if (recipe != null)
            {
                NavService.NavigateTo(new OpenedRecipeViewModel(recipe, L));
            }
        }

        [RelayCommand]
        private void RemoveFromFavorites(Recipe recipe)
        {
            if (recipe != null)
            {
                _recipeDataService.ToggleFavorite(recipe);
                Recipes.Remove(recipe);
            }
        }
    }
}
