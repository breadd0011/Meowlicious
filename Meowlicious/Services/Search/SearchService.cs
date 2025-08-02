using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Meowlicious.Services.Search
{
    public partial class SearchService : ObservableObject, ISearchService
    {
        [ObservableProperty] private string _searchText = string.Empty;

        public event Action? SearchTextChanged;

        partial void OnSearchTextChanged(string value)
        {
            SearchTextChanged?.Invoke();
        }
    }
}
