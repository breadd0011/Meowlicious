using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RecipeApp.Models;
using RecipeApp.Services.Localization;
using RecipeApp.Services.Theme;
using RecipeApp.Utils;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RecipeApp.ViewModels
{
    public partial class SettingsViewModel : ViewModelBase
    {
        [ObservableProperty] private Language _selectedLanguage;
        [ObservableProperty] private IThemeService _themeService;

        private readonly AppSettings _appSettings;
        public ILocalizationService L { get; }

        public SettingsViewModel(AppSettings appSettings, ILocalizationService localizationService, IThemeService themeService)
        {
            L = localizationService;
            _themeService = themeService;
            _appSettings = appSettings;

            _selectedLanguage = Languages.FirstOrDefault(l => l.Code == _appSettings.Language);
        }

        [ObservableProperty]
        private ObservableCollection<Language> _languages = new ObservableCollection<Language>
        {
            new Language
            {
                Name = "English",
                Code = "en",
                FlagPath = "avares://RecipeApp/Assets/Flags/flag_en.png"
            },

            new Language
            {
                Name = "Hungarian",
                Code = "hu",
                FlagPath = $"avares://RecipeApp/Assets/Flags/flag_hu.png"
            }
        };

        private void SaveConfig(string languageCode, bool isDark)
        {
            var config = new
            {
                AppSettings = new { Language = languageCode, IsDark = isDark }
            };

            string json = JsonSerializer.Serialize(
                config,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(Constants.ConfigFile, json);
        }

        [RelayCommand]
        private void ChangeCulture()
        {
            if (SelectedLanguage != null)
            {
                L.ChangeCulture(SelectedLanguage.Code);
                _appSettings.Language = SelectedLanguage.Code;
                SaveConfig(_appSettings.Language, _appSettings.IsDark);
            }
        }

        [RelayCommand]
        private void ChangeTheme()
        {
            ThemeService.SwitchTheme(!ThemeService.IsDarkTheme);
            _appSettings.IsDark = ThemeService.IsDarkTheme;
            SaveConfig(_appSettings.Language, _appSettings.IsDark);
        }
    }
}
