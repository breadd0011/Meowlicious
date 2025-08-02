using System;

namespace RecipeApp.Services.Search
{
    public interface ISearchService
    {
        string SearchText { get; set; }
        event Action? SearchTextChanged;
    }
}
