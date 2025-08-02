using RecipeApp.Services.Localization;

namespace RecipeApp.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public ILocalizationService L { get; }
        public AboutViewModel(ILocalizationService localizationService)
        {
            L = localizationService;
        }
    }
}
