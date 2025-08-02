using Meowlicious.Services.Localization;

namespace Meowlicious.ViewModels
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
