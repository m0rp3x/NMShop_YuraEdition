using NMShop.Shared.Models;

namespace NMShop.Client.Services
{
    public class CartService
    {
        public event Action OnChange;
        private List<CartItem> _items = new List<CartItem>();
        private bool _isCartOpen = false;

        public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
        public bool IsCartOpen => _isCartOpen;

        public void AddProduct(ProductDto product, PriceInfo priceInfo, int quantity = 1)
        {
            var existingItem = _items.FirstOrDefault(item => item.Product.Article == product.Article && item.PriceInfo.Size == priceInfo.Size);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _items.Add(new CartItem { Product = product, PriceInfo = priceInfo, Quantity = quantity });
            }
            NotifyStateChanged();
        }

        public void RemoveProduct(ProductDto product, PriceInfo priceInfo)
        {
            var existingItem = _items.FirstOrDefault(item => item.Product.Article == product.Article && item.PriceInfo.Size == priceInfo.Size);
            if (existingItem != null)
            {
                _items.Remove(existingItem);
                NotifyStateChanged();
            }
        }

        public void UpdateQuantity(ProductDto product, PriceInfo priceInfo, int quantity)
        {
            var existingItem = _items.FirstOrDefault(item => item.Product.Article == product.Article && item.PriceInfo.Size == priceInfo.Size);
            if (existingItem != null && quantity > 0 && quantity < 100)
            {
                existingItem.Quantity = quantity;
                NotifyStateChanged();
            }
        }

        public void ToggleCart()
        {
            _isCartOpen = !_isCartOpen;
            NotifyStateChanged();
        }
        public decimal GetTotalCartPrice() => _items.Sum(item => item.SubTotal);

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    public class CartItem
    {
        public ProductDto Product { get; set; } // Товар
        public PriceInfo PriceInfo { get; set; } // Информация о цене и размере товара
        public int Quantity { get; set; } // Количество товара

        public decimal SubTotal => (PriceInfo.DiscountPrice ?? PriceInfo.Price) * Quantity;
    }
}