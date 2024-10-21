using NMShop.Shared.Models;
using Microsoft.JSInterop;
using Microsoft.Extensions.DependencyInjection;
using NMShop.Shared.Scaffold;

namespace NMShop.Client.Services
{
    public class CartService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ClientDataProvider _dataProvider;
        public event Action OnChange;
        private List<CartItem> _items = new List<CartItem>();
        private bool _isCartOpen = false;
        private PromoCode? _appliedPromoCode;
        private string? _promoCodeError;

        public CartService(IJSRuntime jsRuntime, ClientDataProvider dataProvider)
        {
            _jsRuntime = jsRuntime;
            _dataProvider = dataProvider;
            InitializeCartAsync();
        }

        public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
        public bool IsCartOpen => _isCartOpen;
        public PromoCode? AppliedPromoCode => _appliedPromoCode;
        public string? PromoCodeError => _promoCodeError;

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

        public decimal GetTotalCartPrice()
        {
            decimal total = _items.Sum(item => item.SubTotal);
            if (_appliedPromoCode != null)
            {
                total -= total * _appliedPromoCode.DiscountPercent / 100;
            }
            return total;
        }

        public async Task ApplyPromoCodeAsync(string code)
        {
            _promoCodeError = null;
            _appliedPromoCode = null;

            if (string.IsNullOrWhiteSpace(code))
            {
                _promoCodeError = "Промокод не может быть пустым.";
                NotifyStateChanged();
                return;
            }

            var discount = await _dataProvider.GetPromoCodeDiscountAsync(code);

            if (discount == -1)
            {
                _promoCodeError = "Промокод не найден.";
            }
            else if (discount == 0)
            {
                _promoCodeError = "Промокод истёк или достиг максимального количества использований.";
            }
            else
            {
                _appliedPromoCode = new PromoCode { Code = code, DiscountPercent = discount };
            }

            NotifyStateChanged();
        }

        public async Task SubmitOrderAsync(Order order)
        {
            if (_appliedPromoCode != null)
            {
                order.PromoCode = _appliedPromoCode;
            }

            await _dataProvider.SubmitOrderAsync(order);
            await ClearCartAsync();
            NotifyStateChanged();
        }

        private async Task ClearCartAsync()
        {
            _items.Clear();
            _appliedPromoCode = null;
            _promoCodeError = null;
            await SaveCartToLocalStorageAsync();
        }

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
