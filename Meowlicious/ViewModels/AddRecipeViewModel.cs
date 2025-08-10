using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Meowlicious.Models;
using Meowlicious.Services;
using Meowlicious.Services.FilePicker;
using Meowlicious.Services.Layout;
using Meowlicious.Services.Localization;
using Meowlicious.Services.Navigation;
using Meowlicious.Utils;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Meowlicious.ViewModels
{
    public partial class AddRecipeViewModel : ViewModelBase
    {
        [ObservableProperty] private INavigationService _navService;
        [ObservableProperty] private ILayoutService _layoutService;

        [ObservableProperty] private Recipe _recipeDraft;
        [ObservableProperty] private Ingredient _ingredientDraft;

        [ObservableProperty] private bool _isImgTipVisible;
        [ObservableProperty] private bool _isIngredientStatusTextVisible;
        [ObservableProperty] private bool _isRecipeStatusTextVisible;

        [ObservableProperty] private string _ingredientStatusText;
        [ObservableProperty] private string _recipeStatusText;

        private readonly IRecipeDataService _recipeDataService;
        private readonly IFileService _fileService;
        private readonly Recipe? loadedRecipe;

        public ILocalizationService L { get; }

        public AddRecipeViewModel(
            INavigationService navService,
            ILayoutService layoutService,
            IRecipeDataService recipeDataService,
            ILocalizationService localizationService,
            IFileService fileService,
            Recipe? recipe = null)
        {
            _navService = navService;
            _layoutService = layoutService;
            _recipeDataService = recipeDataService;
            L = localizationService;
            _fileService = fileService;
            loadedRecipe = recipe;

            if (recipe == null)
            {
                RecipeDraft = new Recipe();
                IsImgTipVisible = true;
            }
            else
            {
                RecipeDraft = new Recipe
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Description = recipe.Description,
                    RequiredTime = recipe.RequiredTime,
                    ImageBytes = recipe.ImageBytes?.ToArray(),
                    Ingredients = new ObservableCollection<Ingredient>(
                        recipe.Ingredients.Select(i => new Ingredient
                        {
                            Name = i.Name,
                            Amount = i.Amount,
                            Unit = i.Unit
                        }))
                };

                if (RecipeDraft.ImageBytes != null)
                {
                    IsImgTipVisible = false;
                }
            }

            IngredientDraft = new Ingredient();
        }

        private bool CanAddIngredient()
        {
            return !string.IsNullOrWhiteSpace(IngredientDraft.Name) &&
                   !string.IsNullOrWhiteSpace(IngredientDraft.Unit) &&
                   !string.IsNullOrWhiteSpace(IngredientDraft.Amount);
        }

        private string GetIngredientStatusText()
        {
            if (string.IsNullOrWhiteSpace(IngredientDraft.Name))
            {
                return L["MissingIngredientNameError"];
            }
            if (string.IsNullOrWhiteSpace(IngredientDraft.Unit))
            {
                return L["MissingUnitError"];
            }
            if (string.IsNullOrWhiteSpace(IngredientDraft.Amount))
            {
                return L["MissingAmountError"];
            }

            return string.Empty;
        }

        private bool CanAddRecipe()
        {
            return !string.IsNullOrWhiteSpace(RecipeDraft.Name) &&
                   !string.IsNullOrWhiteSpace(RecipeDraft.RequiredTime);
        }

        private string GetRecipeStatusText()
        {
            if (string.IsNullOrWhiteSpace(RecipeDraft.Name))
            {
                return L["MissingRecipeNameError"];
            }
            if (string.IsNullOrWhiteSpace(RecipeDraft.RequiredTime))
            {
                return L["MissingRequiredTimeError"];
            }
            return string.Empty;
        }

        private byte[] LoadPlaceholderImage()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(Constants.PlaceholderImagePath);
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        [RelayCommand]
        private async Task UploadImage()
        {
            var file = await _fileService.OpenFileAsync();
            if (file is null) return;

            await using var stream = await file.OpenReadAsync();
            using var memoryStream = new MemoryStream();

            await stream.CopyToAsync(memoryStream);
            RecipeDraft.ImageBytes = memoryStream.ToArray();

            if (RecipeDraft.ImageBytes != null)
            {
                IsImgTipVisible = false;
            }

        }

        [RelayCommand]
        private void AddIngredient()
        {
            if (!CanAddIngredient())
            {
                IngredientStatusText = GetIngredientStatusText();
                IsIngredientStatusTextVisible = true;
                return;
            }

            IsIngredientStatusTextVisible = false;
            RecipeDraft.Ingredients.Add(IngredientDraft);
            IngredientDraft = new();
        }

        [RelayCommand]
        private void RemoveIngredient(Ingredient ingredient)
        {
            RecipeDraft.Ingredients.Remove(ingredient);
        }

        [RelayCommand]
        private void AddRecipe()
        {
            if (!CanAddRecipe())
            {
                RecipeStatusText = GetRecipeStatusText();
                IsRecipeStatusTextVisible = true;
                return;
            }

            if (loadedRecipe != null)
            {
                loadedRecipe.Name = RecipeDraft.Name;
                loadedRecipe.Description = RecipeDraft.Description;
                loadedRecipe.RequiredTime = RecipeDraft.RequiredTime;
                loadedRecipe.ImageBytes = RecipeDraft.ImageBytes;
                loadedRecipe.Ingredients = RecipeDraft.Ingredients;

                _recipeDataService.UpdateRecipe(loadedRecipe);
            }
            else
            {
                _recipeDataService.AddRecipe(RecipeDraft);
            }

            if (RecipeDraft.ImageBytes == null)
            {
                RecipeDraft.ImageBytes = LoadPlaceholderImage();
            }

            IsRecipeStatusTextVisible = false;
            NavService.NavigateTo<RecipeExplorerViewModel>();
            LayoutService.CurrentPageType = typeof(RecipeExplorerViewModel);
        }

        [RelayCommand]
        private void CancelRecipe()
        {
            NavService.NavigateTo<RecipeExplorerViewModel>();
            LayoutService.CurrentPageType = typeof(RecipeExplorerViewModel);
        }
    }
}
