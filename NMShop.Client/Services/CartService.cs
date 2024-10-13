using NMShop.Shared.Models;

namespace NMShop.Client.Services
{
    public class CartService
    {
        public event Action OnChange;
        private List<ProductDto> _products = new List<ProductDto>();
        private bool _isCartOpen;

        public IReadOnlyList<ProductDto> Products => _products.AsReadOnly();
        public bool IsCartOpen => _isCartOpen;

        public void AddProduct(ProductDto product)
        {
            _products.Add(product);
            NotifyStateChanged();
        }

        public void RemoveProduct(ProductDto product)
        {
            _products.Remove(product);
            NotifyStateChanged();
        }

        public void ToggleCart()
        {
            _isCartOpen = !_isCartOpen;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}
