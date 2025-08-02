using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using System;

namespace RecipeApp.Services.Theme
{
    public class ThemeService : IThemeService
    {
        public bool IsDarkTheme { get; private set; }

        public void SwitchTheme(bool isDark)
        {
            IsDarkTheme = isDark;

            Application.Current.Resources.MergedDictionaries.Clear();

            var themeSource = new ResourceInclude(new Uri("avares://RecipeApp/Styles/Themes/"))
            {
                Source = new Uri($"avares://RecipeApp/Styles/Themes/{(isDark ? "Dark" : "Light")}Theme.axaml")
            };

            Application.Current.Resources.MergedDictionaries.Add(themeSource);
        }
    }
}
