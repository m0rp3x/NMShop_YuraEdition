using NMShop.Shared.Models;
using Microsoft.JSInterop;
using Microsoft.Extensions.DependencyInjection;

namespace NMShop.Client.Services
{
    public class CartService
    {
        private readonly IJSRuntime _jsRuntime;
        public event Action OnChange;
        private List<CartItem> _items = new List<CartItem>();
        private bool _isCartOpen = false;

        public CartService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            InitializeCartAsync();
        }

        public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
        public bool IsCartOpen => _isCartOpen;

        public async Task InitializeCartAsync()
        {
            var savedCart = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "cartItems");
            if (!string.IsNullOrEmpty(savedCart))
            {
                _items = System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(savedCart) ?? new List<CartItem>();
                NotifyStateChanged();
            }
        }

        public async void AddProduct(ProductDto product, PriceInfo priceInfo, int quantity = 1)
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
            await SaveCartToLocalStorageAsync();
            NotifyStateChanged();
        }

        public async void RemoveProduct(ProductDto product, PriceInfo priceInfo)
        {
            var existingItem = _items.FirstOrDefault(item => item.Product.Article == product.Article && item.PriceInfo.Size == priceInfo.Size);
            if (existingItem != null)
            {
                _items.Remove(existingItem);
                await SaveCartToLocalStorageAsync();
                NotifyStateChanged();
            }
        }

        public async void UpdateQuantity(ProductDto product, PriceInfo priceInfo, int quantity)
        {
            var existingItem = _items.FirstOrDefault(item => item.Product.Article == product.Article && item.PriceInfo.Size == priceInfo.Size);
            if (existingItem != null && quantity > 0 && quantity < 100)
            {
                existingItem.Quantity = quantity;
                await SaveCartToLocalStorageAsync();
                NotifyStateChanged();
            }
        }

        public void ToggleCart()
        {
            _isCartOpen = !_isCartOpen;
            NotifyStateChanged();
        }

        public decimal GetTotalCartPrice() => _items.Sum(item => item.SubTotal);

        private async Task SaveCartToLocalStorageAsync()
        {
            var serializedCart = System.Text.Json.JsonSerializer.Serialize(_items);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "cartItems", serializedCart);
        }

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
