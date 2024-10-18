using NMShop.Shared.Models;
using System.Net.Http.Json;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using NMShop.Shared.Scaffold;

namespace NMShop.Client.Services
{
    public class ClientDataProvider
    {
        private readonly HttpClient _http;
        private readonly Dictionary<string, (DateTime, object)> _cache = new();
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

        public ClientDataProvider(HttpClient http)
        {
            _http = http;
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
            _cache[key] = (DateTime.Now, value);
        }

        public async Task<IEnumerable<Brand>> GetBrandsAsync()
        {
            var cacheKey = "brands";
            var cachedData = GetFromCache<IEnumerable<Brand>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<Brand>>("https://localhost:7279/api/productattributes/brands");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<Gender>> GetGendersAsync()
        {
            var cacheKey = "genders";
            var cachedData = GetFromCache<IEnumerable<Gender>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<Gender>>("https://localhost:7279/api/productattributes/genders");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<ProductType>> GetSubCategoriesAsync(int? parentCategoryId = null)
        {
            var cacheKey = parentCategoryId == null ? "subCategories" : $"subCategories_{parentCategoryId}";
            var cachedData = GetFromCache<IEnumerable<ProductType>>(cacheKey);
            if (cachedData != null) return cachedData;

            string url = "https://localhost:7279/api/productattributes/product-types";
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

            var data = await _http.GetFromJsonAsync<IEnumerable<ProductColor>>("https://localhost:7279/api/productattributes/colors");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<string> GetCategorySizeDisplayTypeAsync(int categoryId)
        {
            var cacheKey = $"categorySizeDisplayType_{categoryId}";
            var cachedData = GetFromCache<string>(cacheKey);
            if (cachedData != null) return cachedData;

            string url = $"https://localhost:7279/api/productattributes/category-size-display-type?categoryId={categoryId}";

            var data = await _http.GetStringAsync(url);
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<SellingCategory>> GetSellingCategoriesAsync()
        {
            var cacheKey = "sellingCategories";
            var cachedData = GetFromCache<IEnumerable<SellingCategory>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<SellingCategory>>("https://localhost:7279/api/productattributes/selling-categories");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var cacheKey = $"product_{id}";
            var cachedData = GetFromCache<ProductDto>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<ProductDto>($"https://localhost:7279/api/products/id/{id}");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<ProductDto> GetProductByArticleAsync(string article)
        {
            var cacheKey = $"productByArticle_{article}";
            var cachedData = GetFromCache<ProductDto>(cacheKey);
            if (cachedData != null) return cachedData;

            string url = $"https://localhost:7279/api/products/product-by-article?article={HttpUtility.UrlEncode(article)}";
            var product = await _http.GetFromJsonAsync<ProductDto>(url);
            SetCache(cacheKey, product);
            return product;
        }

        public async Task<IEnumerable<ReferenceInfo>> GetReferenceInfo(string topic = null)
        {
            var cacheKey = topic == null ? "referenceInfo" : $"referenceInfo_{topic}";
            var cachedData = GetFromCache<IEnumerable<ReferenceInfo>>(cacheKey);
            if (cachedData != null) return cachedData;

            string url = topic == null
                ? "https://localhost:7279/api/referenceinfo"
                : $"https://localhost:7279/api/referenceinfo/{HttpUtility.UrlEncode(topic)}";

            var data = await _http.GetFromJsonAsync<IEnumerable<ReferenceInfo>>(url);
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<ProductDto>> GetFilteredProducts(ProductFilter filter)
        {
            var url = "https://localhost:7279/api/products/filter";
            var response = await _http.PostAsJsonAsync(url, filter);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
        }

        public async Task<int?> GetCategoryIdByNameAsync(string categoryName)
        {
            var cacheKey = $"categoryId_{categoryName}";
            var cachedData = GetFromCache<int?>(cacheKey);
            if (cachedData != null) return cachedData;

            var url = $"https://localhost:7279/api/productattributes/category-id-by-name?categoryName={HttpUtility.UrlEncode(categoryName)}";
            var data = await _http.GetFromJsonAsync<int?>(url);
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<ContactMethod>> GetContactMethodsAsync()
        {
            var cacheKey = "contactMethods";
            var cachedData = GetFromCache<IEnumerable<ContactMethod>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<ContactMethod>>("https://localhost:7279/api/orders/contact-methods");
            SetCache(cacheKey, data);
            return data;
        }
        public async Task<IEnumerable<DeliveryType>> GetDeliveryTypesAsync()
        {
            var cacheKey = "deliveryTypes";
            var cachedData = GetFromCache<IEnumerable<DeliveryType>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<DeliveryType>>("https://localhost:7279/api/orders/delivery-types");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<PaymentType>> GetPaymentTypesAsync()
        {
            var cacheKey = "paymentTypes";
            var cachedData = GetFromCache<IEnumerable<PaymentType>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<PaymentType>>("https://localhost:7279/api/orders/payment-types");
            SetCache(cacheKey, data);
            return data;
        }
    }
}