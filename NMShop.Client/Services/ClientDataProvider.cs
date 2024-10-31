using NMShop.Shared.Models;
using System.Net.Http.Json;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using NMShop.Shared.Scaffold;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace NMShop.Client.Services
{
    public class ClientDataProvider
    {
        private readonly HttpClient _http;
        private readonly Dictionary<string, (DateTime, object)> _cache = new();
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

        // Base URL configuration
        private readonly string _baseUrl;

        public ClientDataProvider(HttpClient http, IServiceProvider serviceProvider)
        {
            _http = http;

            if (serviceProvider.GetService<IWebAssemblyHostEnvironment>() is not null)
            {
                var environment = serviceProvider.GetRequiredService<IWebAssemblyHostEnvironment>();
                _baseUrl = environment.IsDevelopment()
                    ? "http://localhost:5000"
                    : "https://www.kickrooms.ru";
            }
            else if (serviceProvider.GetService<IHostingEnvironment>() is not null)
            {
                var environment = serviceProvider.GetRequiredService<IHostingEnvironment>();
                _baseUrl = environment.IsDevelopment()
                    ? "http://localhost:5000"
                    : "https://www.kickrooms.ru";
            }
            else
            {
                // Default fallback
                _baseUrl = "https://www.kickrooms.ru";
            }
        }

        private T GetFromCache<T>(string key)
        {
            if (_cache.TryGetValue(key, out var cacheEntry))
            {
                if (DateTime.Now - cacheEntry.Item1 < _cacheExpiration)
                {
                    return (T)cacheEntry.Item2;
                }
                _cache.Remove(key);
            }
            return default;
        }

        private void SetCache<T>(string key, T value)
        {
            if (value != null)
            {
                _cache[key] = (DateTime.Now, value);
            }
        }

        public async Task<IEnumerable<NavigationUnit>> GetNavigationUnitsAsync()
        {
            var cacheKey = "navigationUnits";
            var cachedData = GetFromCache<IEnumerable<NavigationUnit>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<NavigationUnit>>($"{_baseUrl}/api/navigation/GetAllNavigationUnits");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<BrandGalleryItem>> GetBrandGalleriesAsync()
        {
            var cacheKey = "brandGalleries";
            var cachedData = GetFromCache<IEnumerable<BrandGalleryItem>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<BrandGalleryItem>>($"{_baseUrl}/api/navigation/GetAllBrandGalleries");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<Brand>> GetBrandsAsync()
        {
            var cacheKey = "brands";
            var cachedData = GetFromCache<IEnumerable<Brand>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<Brand>>($"{_baseUrl}/api/productattributes/brands");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<Gender>> GetGendersAsync()
        {
            var cacheKey = "genders";
            var cachedData = GetFromCache<IEnumerable<Gender>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<Gender>>($"{_baseUrl}/api/productattributes/genders");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<ProductType>> GetSubCategoriesAsync(int? parentCategoryId = null)
        {
            var cacheKey = parentCategoryId == null ? "subCategories" : $"subCategories_{parentCategoryId}";
            var cachedData = GetFromCache<IEnumerable<ProductType>>(cacheKey);
            if (cachedData != null) return cachedData;

            string url = $"{_baseUrl}/api/productattributes/product-types";
            if (parentCategoryId.HasValue)
            {
                url += $"?parentCategoryId={parentCategoryId}";
            }

            var data = await _http.GetFromJsonAsync<IEnumerable<ProductType>>(url);
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<ProductColor>> GetProductColorsAsync()
        {
            var cacheKey = "productColors";
            var cachedData = GetFromCache<IEnumerable<ProductColor>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<ProductColor>>($"{_baseUrl}/api/productattributes/colors");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<string> GetCategorySizeDisplayTypeAsync(int categoryId)
        {
            var cacheKey = $"categorySizeDisplayType_{categoryId}";
            var cachedData = GetFromCache<string>(cacheKey);
            if (cachedData != null) return cachedData;

            string url = $"{_baseUrl}/api/productattributes/category-size-display-type?categoryId={categoryId}";

            var data = await _http.GetStringAsync(url);
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<SellingCategory>> GetSellingCategoriesAsync()
        {
            var cacheKey = "sellingCategories";
            var cachedData = GetFromCache<IEnumerable<SellingCategory>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<SellingCategory>>($"{_baseUrl}/api/productattributes/selling-categories");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var cacheKey = $"product_{id}";
            var cachedData = GetFromCache<ProductDto>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<ProductDto>($"{_baseUrl}/api/products/id/{id}");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<ProductDto> GetProductByArticleAsync(string article)
        {
            var cacheKey = $"productByArticle_{article}";
            var cachedData = GetFromCache<ProductDto>(cacheKey);
            if (cachedData != null) return cachedData;

            string url = $"{_baseUrl}/api/products/product-by-article?article={HttpUtility.UrlEncode(article)}";
            var product = await _http.GetFromJsonAsync<ProductDto>(url);
            SetCache(cacheKey, product);
            return product;
        }

        public async Task<IEnumerable<ReferenceTopic>> GetChildTopicsByParentCodeAsync(string parentCode)
        {
            string url = $"{_baseUrl}/api/referenceinfo/{HttpUtility.UrlEncode(parentCode)}/children";
            return await _http.GetFromJsonAsync<IEnumerable<ReferenceTopic>>(url);
        }

        public async Task<ReferenceTopic> GetReferenceInfoByTopicAsync(string topic)
        {
            string url = $"{_baseUrl}/api/referenceinfo/{HttpUtility.UrlEncode(topic)}";
            return await _http.GetFromJsonAsync<ReferenceTopic>(url);
        }

        public async Task<IEnumerable<ReferenceTopic>> GetAllReferenceInfoAsync()
        {
            string url = $"{_baseUrl}/api/referenceinfo";
            return await _http.GetFromJsonAsync<IEnumerable<ReferenceTopic>>(url);
        }

        public async Task<IEnumerable<ProductDto>> GetFilteredProducts(ProductFilter filter)
        {
            var url = $"{_baseUrl}/api/products/filter";
            var response = await _http.PostAsJsonAsync(url, filter);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
        }

        public async Task<int?> GetCategoryIdByNameAsync(string categoryName)
        {
            var cacheKey = $"categoryId_{categoryName}";
            var cachedData = GetFromCache<int?>(cacheKey);
            if (cachedData != null) return cachedData;

            var url = $"{_baseUrl}/api/productattributes/category-id-by-name?categoryName={HttpUtility.UrlEncode(categoryName)}";
            var data = await _http.GetFromJsonAsync<int?>(url);
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<int> GetFilteredProductsCount(ProductFilter filter)
        {
            var url = $"{_baseUrl}/api/products/filter-count";
            var response = await _http.PostAsJsonAsync(url, filter);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<IEnumerable<ContactMethod>> GetContactMethodsAsync()
        {
            var cacheKey = "contactMethods";
            var cachedData = GetFromCache<IEnumerable<ContactMethod>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<ContactMethod>>($"{_baseUrl}/api/orders/contact-methods");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<DeliveryType>> GetDeliveryTypesAsync()
        {
            var cacheKey = "deliveryTypes";
            var cachedData = GetFromCache<IEnumerable<DeliveryType>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<DeliveryType>>($"{_baseUrl}/api/orders/delivery-types");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<PaymentType>> GetPaymentTypesAsync()
        {
            var cacheKey = "paymentTypes";
            var cachedData = GetFromCache<IEnumerable<PaymentType>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<PaymentType>>($"{_baseUrl}/api/orders/payment-types");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<int?> GetBrandIdByNameAsync(string brandName)
        {
            var cacheKey = $"brandId_{brandName}";
            var cachedData = GetFromCache<int?>(cacheKey);
            if (cachedData != null) return cachedData;

            if (string.IsNullOrEmpty(brandName))
            {
                Console.Error.WriteLine("Brand name cannot be null or empty.");
            }

            var url = $"{_baseUrl}/api/productattributes/brand-id-by-name?brandName={HttpUtility.UrlEncode(brandName)}";
            var data = await _http.GetFromJsonAsync<int?>(url);
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<int> GetPromoCodeDiscountAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                Console.Error.WriteLine("Promo code cannot be null or empty.");
            }

            var url = $"{_baseUrl}/api/orders/promo-code/{HttpUtility.UrlEncode(code)}";
            return await _http.GetFromJsonAsync<int>(url);
        }

        public async Task<(bool isSuccess, string message)> SubmitOrderAsync(CreateOrderDto order)
        {
            if (order == null)
            {
                return (false, "Заказ не может быть пустым.");
            }

            try
            {
                var response = await _http.PostAsJsonAsync($"{_baseUrl}/api/orders/submit-order", order);

                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return (false, $"{response.StatusCode}, {content}");
                }

                return (true, "Заказ успешно отправлен.");
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка: {ex.Message}");
            }
        }
    }
}