using NMShop.Shared.Models;
using Microsoft.JSInterop;
using Microsoft.Extensions.DependencyInjection;
using NMShop.Shared.Scaffold;
using static NMShop.Client.Pages.Checkout;
using System.Text.Json;

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
        private CheckoutForm? _checkoutForm; // Поле для формы

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
        public CheckoutForm? CheckoutForm => _checkoutForm; // Предоставление формы

        // Метод для инициализации корзины и формы из localStorage
        public async Task InitializeCartAsync()
        {
            var savedCart = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "cartItems");
            if (!string.IsNullOrEmpty(savedCart))
            {
                _items = JsonSerializer.Deserialize<List<CartItem>>(savedCart) ?? new List<CartItem>();
            }

            var savedCheckoutForm = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "checkoutForm");
            if (!string.IsNullOrEmpty(savedCheckoutForm))
            {
                _checkoutForm = JsonSerializer.Deserialize<CheckoutForm>(savedCheckoutForm) ?? new CheckoutForm();
            }

            NotifyStateChanged();
        }

        // Метод для сохранения формы в localStorage
        public async Task SaveCheckoutFormAsync(CheckoutForm form)
        {
            _checkoutForm = form;
            var serializedForm = JsonSerializer.Serialize(_checkoutForm);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "checkoutForm", serializedForm);
        }

        // Метод для очистки формы и корзины
        private async Task ClearCartAsync()
        {
            _items.Clear();
            _appliedPromoCode = null;
            _promoCodeError = null;
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "cartItems");
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

        public decimal GetTotalCartPriceWithoutDiscount()
        {
            return _items.Sum(item => item.SubTotal);
        }

        public decimal GetTotalDiscount()
        {
            if (_appliedPromoCode != null)
            {
                var totalWithoutDiscount = GetTotalCartPriceWithoutDiscount();
                return totalWithoutDiscount * _appliedPromoCode.DiscountPercent / 100;
            }
            return 0;
        }

        public decimal GetTotalCartPrice()
        {
            decimal total = GetTotalCartPriceWithoutDiscount();
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

            if (discount < 0)
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

        // Метод для отправки заказа
        public async Task<(bool isSuccess, string message)> SubmitOrderAsync(CheckoutForm checkoutForm)
        {
            var order = await BuildOrderAsync(checkoutForm);

            var (isSuccess, message) = await _dataProvider.SubmitOrderAsync(order);

            if (isSuccess)
            {
                await ClearCartAsync();
                NotifyStateChanged();
            }

            return (isSuccess, message);
        }

        private async Task SaveCartToLocalStorageAsync()
        {
            var serializedCart = JsonSerializer.Serialize(_items);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "cartItems", serializedCart);
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        // Метод для построения объекта заказа на основе формы
        public async Task<OrderCreateDto> BuildOrderAsync(CheckoutForm checkoutForm)
        {
            var order = new OrderCreateDto
            {
                ClientFullName = checkoutForm.FIO,
                DeliveryAdress = checkoutForm.Address,
                DeliveryTypeId = checkoutForm.selectedDeliveryMethod.Id,
                PaymentTypeId = checkoutForm.selectedPaymentMethod.Id,
                ContactMethodId = checkoutForm.selectedContactMethod.Id,
                ContactValue = checkoutForm.Contact,
                DeliveryRecipientFullName = checkoutForm.IsClientRecipient ? checkoutForm.FIO : checkoutForm.Recipient_FIO,
                DeliveryRecipientPhone = checkoutForm.IsClientRecipient ? checkoutForm.Contact : checkoutForm.Recipient_Phone,
                OrderParts = _items.Select(item => new OrderPartDto
                {
                    ProductId = item.Product.Id,
                    Amount = item.Quantity,
                }).ToList(),
                PromoCode = AppliedPromoCode?.Code
            };

            return order;
        }
    }

    public class CartItem
    {
        public ProductDto Product { get; set; } // Товар
        public PriceInfo PriceInfo { get; set; } // Информация о цене и размере товара
        public int Quantity { get; set; } // Количество товара

        public decimal SubTotal => (PriceInfo.DiscountPrice ?? PriceInfo.Price) * Quantity;
    }
}
