using CommunityToolkit.Mvvm.ComponentModel;
using Meowlicious.Models;
using Meowlicious.Services.Localization;

namespace Meowlicious.ViewModels
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
