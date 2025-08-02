using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace Meowlicious.Services.FilePicker
{
    public class FileService : IFileService
    {
        private Window _mainWindow;

        public void Initialize(Window mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public async Task<IStorageFile?> OpenFileAsync()
        {
            var files = await _mainWindow.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
            {
                Title = "Choose an image",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("Image Files")
                    {
                        Patterns = new[]
                        {
                            "*.jpg",
                            "*.jpeg",
                            "*.png",
                            "*.webp",
                        }
                    }
                }
            });

            return files.Count >= 1 ? files[0] : null;
        }
    }
}
