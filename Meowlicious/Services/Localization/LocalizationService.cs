using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace RecipeApp.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ResourceManager _resourceManager;
        private CultureInfo _currentCulture;

        public event PropertyChangedEventHandler? PropertyChanged;

        public LocalizationService()
        {
            _resourceManager = new ResourceManager("RecipeApp.Resources.Strings", typeof(LocalizationService).Assembly);
            _currentCulture = CultureInfo.CurrentUICulture;
        }

        public string this[string key] => _resourceManager.GetString(key, _currentCulture) ?? $"!!{key}!!";

        public void ChangeCulture(string cultureCode)
        {
            _currentCulture = new CultureInfo(cultureCode);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        public CultureInfo CurrentCulture => _currentCulture;
    }
}
