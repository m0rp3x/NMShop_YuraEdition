using Microsoft.JSInterop;
using NMShop.Shared.Models;
using NMShop.Shared.Scaffold;

namespace NMShop.Client.Services
{
    public class SearchService
    {
        private readonly ClientDataProvider _dataProvider;
        public event Action OnChange;
        private bool _isSearchOpen = false;
        public bool IsSearchOpen => _isSearchOpen;

        public SearchService(ClientDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
        public void ToggleSearch()
        {
            _isSearchOpen = !_isSearchOpen;
            NotifyStateChanged();
        }

    }
}
