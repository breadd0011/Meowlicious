namespace Meowlicious.Services.Theme
{
    public interface IThemeService
    {
        bool IsDarkTheme { get; }
        void SwitchTheme(bool isDark);
    }
}
