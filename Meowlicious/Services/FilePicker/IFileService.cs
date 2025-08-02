using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace Meowlicious.Services.FilePicker
{
    public interface IFileService
    {
        void Initialize(Window mainWindow);
        public Task<IStorageFile?> OpenFileAsync();
    }
}
