using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Services
{
    public class RecipeDataService : IRecipeDataService
    {
        private readonly RecipeDbContext _context;

        public RecipeDataService(RecipeDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public ObservableCollection<Recipe> GetAllRecipes()
        {
            _context.Recipes.Include(r => r.Ingredients).Load();
            return _context.Recipes.Local.ToObservableCollection();
        }
        public ObservableCollection<Recipe> GetFavoriteRecipes()
        {
            _context.Recipes
                .Include(r => r.Ingredients)
                .Where(r => r.IsFavorite)
                .Load();

            return new ObservableCollection<Recipe>(
                _context.Recipes.Local.Where(r => r.IsFavorite));
        }

        public void AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            _context.SaveChanges();
        }

        public void ToggleFavorite(Recipe recipe)
        {
            var existingRecipe = _context.Recipes.FirstOrDefault(r => r.Id == recipe.Id);
            if (existingRecipe != null)
            {
                existingRecipe.IsFavorite = !existingRecipe.IsFavorite;
                _context.SaveChanges();
            }
        }

        public void DeleteRecipe(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
        }
    }
}
