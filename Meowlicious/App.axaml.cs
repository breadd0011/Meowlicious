using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Meowlicious.Data;
using Meowlicious.Services;
using Meowlicious.Services.FilePicker;
using Meowlicious.Services.Localization;
using Meowlicious.Services.Navigation;
using Meowlicious.Services.Page;
using Meowlicious.Services.Search;
using Meowlicious.Services.Theme;
using Meowlicious.Utils;
using Meowlicious.ViewModels;
using Meowlicious.Views;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace Meowlicious
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var services = new ServiceCollection();

            // Ensure config file exists before building configuration
            EnsureConfigFileExists();

            // Build configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json")
                .Build();

            var appSettings = new AppSettings();
            config.GetSection("AppSettings").Bind(appSettings);
            services.AddSingleton(appSettings);

            // Set culture
            Thread.CurrentThread.CurrentCulture = new CultureInfo(appSettings.Language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(appSettings.Language);

            // Register other services
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<RecipeExplorerViewModel>();
            services.AddSingleton<FavoritesViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<AboutViewModel>();
            services.AddTransient<AddRecipeViewModel>();
            services.AddTransient<OpenedRecipeViewModel>();

            services.AddSingleton<ILocalizationService, LocalizationService>();
            services.AddSingleton<IRecipeDataService, RecipeDataService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ISearchService, SearchService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<IThemeService, ThemeService>();

            services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

            services.AddDbContext<RecipeDbContext>();

            var serviceProvider = services.BuildServiceProvider();

            // Change the theme based on app settings
            serviceProvider.GetRequiredService<IThemeService>().SwitchTheme(appSettings.IsDark);

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>()
                };

                var fileService = serviceProvider.GetRequiredService<IFileService>();
                fileService.Initialize(desktop.MainWindow);
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }

        private void EnsureConfigFileExists()
        {
            if (!File.Exists(Constants.ConfigFile))
            {
                var defaultConfig = new
                {
                    AppSettings = new { Language = "en", IsDark = true }
                };

                File.WriteAllText(
                    Constants.ConfigFile,
                    JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true })
                );
            }
        }
    }
}