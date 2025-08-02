using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RecipeApp.Models
{
    public partial class Recipe : ObservableObject
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? RequiredTime { get; set; }

        [ObservableProperty]
        private bool _isFavorite;
        public string? Making { get; set; }

        [ObservableProperty]
        private byte[]? _imageBytes;

        public ObservableCollection<Ingredient> Ingredients { get; set; } = new();
    }
}
