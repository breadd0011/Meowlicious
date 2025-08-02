using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Amount { get; set; }
        public string? Unit { get; set; }

        public int RecipeId { get; set; } // Foreign key
        public Recipe? Recipe { get; set; }
    }
}
