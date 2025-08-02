using RecipeApp.ViewModels;

namespace RecipeApp.Services.Navigation
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }
        void NavigateTo<T>() where T : ViewModelBase;
        void NavigateTo(ViewModelBase ViewModel);
    }
}
