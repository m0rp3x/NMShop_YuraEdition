namespace NMShop.Client.Services
{
    public class LayoutService
    {
        public bool IsSearchOpen = false;
        public bool IsCartOpen = false;
        public event Action OnChange;
        public bool LockScroll => IsSearchOpen || IsCartOpen;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void ToggleSearch() { 
            IsSearchOpen = !IsSearchOpen; 
            Console.WriteLine($"IsSearchOpen = {IsSearchOpen}");
            NotifyStateChanged();
        }
        public void ToggleCart() { 
            IsCartOpen = !IsCartOpen; 
            Console.WriteLine($"IsCartOpen = {IsCartOpen}");
            NotifyStateChanged();
        }
    }
}
