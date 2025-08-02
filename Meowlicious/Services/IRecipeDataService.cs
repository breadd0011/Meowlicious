using System.Collections.ObjectModel;
using Meowlicious.Models;

namespace Meowlicious.Services
{
    public interface IRecipeDataService
    {
        ObservableCollection<Recipe> GetAllRecipes();
        ObservableCollection<Recipe> GetFavoriteRecipes();
        void AddRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void ToggleFavorite(Recipe recipe);
        void DeleteRecipe(Recipe recipe);
    }
}
