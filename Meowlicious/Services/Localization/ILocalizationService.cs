using System.ComponentModel;
using System.Globalization;

namespace Meowlicious.Services.Localization
{
    public interface ILocalizationService : INotifyPropertyChanged
    {
        string this[string key] { get; }
        void ChangeCulture(string cultureCode);
        CultureInfo CurrentCulture { get; }
    }
}
