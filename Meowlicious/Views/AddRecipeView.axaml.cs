using Avalonia.Controls;
using Avalonia.Input;

namespace RecipeApp.Views;

public partial class AddRecipeView : UserControl
{
    public AddRecipeView()
    {
        InitializeComponent();
    }

    private void OnNumericTextBoxKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.Left || e.Key == Key.Right ||
            e.Key == Key.Tab || e.KeyModifiers.HasFlag(KeyModifiers.Control))
        {
            return;
        }

        if (!e.KeyModifiers.HasFlag(KeyModifiers.Shift) &&
            ((e.Key >= Key.D0 && e.Key <= Key.D9) ||
             (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)))
        {
            return;
        }

        e.Handled = true;
    }

    private void OnPastingFromClipboard(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        e.Handled = true;
    }
}

