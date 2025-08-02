using System.Threading.Tasks;
using Velopack;

namespace RecipeApp.Utils
{
    public class AutoUpdater
    {
        public static UpdateManager UpdateManager = new(Constants.GitHubRepoUrl);
        public static UpdateInfo? NewVersion;
        public static bool UpdateAvailable => NewVersion != null;

        public static async Task CheckForUpdatesAsync()
        {
            UpdateManager = new UpdateManager(Constants.GitHubRepoUrl);

            NewVersion = await UpdateManager.CheckForUpdatesAsync();

            if (NewVersion == null) return;
        }

        public static async Task DownloadUpdateAsync()
        {
            if (NewVersion != null) await UpdateManager.DownloadUpdatesAsync(NewVersion);
        }

        public static void UpdateAndRestartApp()
        {
            if (NewVersion != null) UpdateManager.ApplyUpdatesAndRestart(NewVersion);
        }
    }
}
