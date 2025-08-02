using System;

namespace Meowlicious.Services.Search
{
    public interface ISearchService
    {
        string SearchText { get; set; }
        event Action? SearchTextChanged;
    }
}
