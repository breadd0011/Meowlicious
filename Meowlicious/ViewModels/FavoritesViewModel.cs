using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Meowlicious.Models;
using Meowlicious.Services;
using Meowlicious.Services.FilePicker;
using Meowlicious.Services.Localization;
using Meowlicious.Services.Navigation;
using Meowlicious.Services.Page;
using Meowlicious.Services.Search;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Meowlicious.ViewModels
{
    public partial class FavoritesViewModel : ViewModelBase
    {
        [ObservableProperty] private INavigationService _navService;
        [ObservableProperty] private ISidebarService _SidebarService;

        [ObservableProperty] private ObservableCollection<Recipe> _recipes;
        [ObservableProperty] private ObservableCollection<Recipe> _filteredRecipes;

        [ObservableProperty] private bool _isPopupOpen;
        [ObservableProperty] private bool _isNoRecipesTextVisible;
        [ObservableProperty] private bool _isNoSearchTextVisible;

        [ObservableProperty] private Recipe _selectedRecipe;

        private readonly IRecipeDataService _recipeDataService;
        private readonly ISearchService _searchService;
        private readonly IFileService _fileService;
        public ILocalizationService L { get; }
        public FavoritesViewModel(
            INavigationService navService,
            ISidebarService SidebarService,
            IRecipeDataService recipeDataService,
            ILocalizationService localizationService,
            ISearchService searchService,
            IFileService fileService)
        {
            _navService = navService;
            _SidebarService = SidebarService;
            _recipeDataService = recipeDataService;
            _searchService = searchService;
            L = localizationService;
            _fileService = fileService;

            Recipes = _recipeDataService.GetFavoriteRecipes();
            FilteredRecipes = new ObservableCollection<Recipe>(Recipes);

            Recipes.CollectionChanged += OnRecipesCollectionChanged;
            _searchService.SearchTextChanged += UpdateState;

            UpdateState();
        }

        private void UpdateState()
        {
            var filtered = ApplySearchFilter(Recipes, _searchService.SearchText?.Trim());
            SyncCollections(FilteredRecipes, filtered);

            IsNoSearchTextVisible = FilteredRecipes.Count == 0 && Recipes.Count != 0;
            IsNoRecipesTextVisible = Recipes.Count == 0;
        }

        private void SyncCollections(ObservableCollection<Recipe> target, IEnumerable<Recipe> source)
        {
            var sourceList = source.ToList();

            if (target.Count == sourceList.Count && target.SequenceEqual(sourceList))
                return;

            target.Clear();
            foreach (var item in sourceList)
            {
                target.Add(item);
            }
        }

        private IEnumerable<Recipe> ApplySearchFilter(IEnumerable<Recipe> recipes, string? searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return recipes;
            }

            return recipes.Where(r => !string.IsNullOrWhiteSpace(r.Name) &&
                                      r.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }

        private void OnRecipesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateState();
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
        private void SwitchFavorite(Recipe recipe)
        {
            if (recipe != null)
            {
                _recipeDataService.ToggleFavorite(recipe);

                if (recipe.IsFavorite && Recipes.Contains(recipe))
                {
                    Recipes.Add(recipe);
                }
                else if (!recipe.IsFavorite && Recipes.Contains(recipe))
                {
                    Recipes.Remove(recipe);
                }
            }
        }

        [RelayCommand]
        private void RemoveRecipe(Recipe recipe)
        {
            if (recipe != null)
            {
                SelectedRecipe = recipe;
            }

            if (IsPopupOpen && SelectedRecipe != null)
            {
                _recipeDataService.DeleteRecipe(SelectedRecipe);
                Recipes.Remove(SelectedRecipe);

                if (Recipes.Contains(SelectedRecipe))
                {
                    Recipes.Remove(SelectedRecipe);
                }

                IsPopupOpen = false;
                SelectedRecipe = null;
            }

            else
            {
                IsPopupOpen = true;
            }
        }

        [RelayCommand]
        private void CancelRemove()
        {
            IsPopupOpen = false;
        }

        [RelayCommand]
        private void EditRecipe(Recipe recipe)
        {
            if (recipe != null)
            {
                NavService.NavigateTo(new AddRecipeViewModel(NavService, _SidebarService, _recipeDataService, L, _fileService, recipe));
                SidebarService.CurrentPageType = typeof(OpenedRecipeViewModel);
            }
        }
    }
}
