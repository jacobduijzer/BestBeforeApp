using System;

namespace BestBeforeApp.Shared
{
    public interface ISearchPage
    {
        void OnSearchBarTextChanged(in string text);
        event EventHandler<string> SearchBarTextChanged;
    }
}
