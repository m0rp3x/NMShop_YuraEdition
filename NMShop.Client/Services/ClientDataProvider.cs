using NMShop.Shared.Models;
using System.Net.Http.Json;
using System.Web;
using Microsoft.Extensions.DependencyInjection;

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

        public async Task<IEnumerable<string>> GetBrandsAsync()
        {
            var cacheKey = "brands";
            var cachedData = GetFromCache<IEnumerable<string>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<string>>("https://localhost:7279/api/productattributes/brands");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<string>> GetGendersAsync()
        {
            var cacheKey = "genders";
            var cachedData = GetFromCache<IEnumerable<string>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<string>>("https://localhost:7279/api/productattributes/genders");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<string>> GetSubCategoriesAsync(string parentCategory = null)
        {
            var cacheKey = parentCategory == null ? "subCategories" : $"subCategories_{parentCategory}";
            var cachedData = GetFromCache<IEnumerable<string>>(cacheKey);
            if (cachedData != null) return cachedData;

            string url = "https://localhost:7279/api/productattributes/product-types";
            if (!string.IsNullOrEmpty(parentCategory))
            {
                url += $"?parentCategory={HttpUtility.UrlEncode(parentCategory)}";
            }

            var data = await _http.GetFromJsonAsync<IEnumerable<string>>(url);
            SetCache(cacheKey, data);
            return data;
        }
        
        public async Task<IEnumerable<ProductColorDto>> GetProductColorsAsync()
        {
            var cacheKey = "productColors";
            var cachedData = GetFromCache<IEnumerable<ProductColorDto>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<ProductColorDto>>("https://localhost:7279/api/productattributes/colors");
            SetCache(cacheKey, data);
            return data;
        }
        
        public async Task<IEnumerable<ProductDto>> GetFilteredProductsByColor(ProductFilter filter, string color)
        {
            var queryString = filter.ToQueryString();
            var url = $"https://localhost:7279/api/products/filter?{queryString}&Color={HttpUtility.UrlEncode(color)}";
            return await _http.GetFromJsonAsync<IEnumerable<ProductDto>>(url);
        }

        public async Task<string> GetCategorySizeDisplayTypeAsync(string category)
                 {
                     var cacheKey = $"categorySizeDisplayType_{category}";
                     var cachedData = GetFromCache<string>(cacheKey);
                     if (cachedData != null) return cachedData;
         
                     string url = "https://localhost:7279/api/productattributes/category-size-display-type";
                     if (!string.IsNullOrEmpty(category))
                     {
                         url += $"?category={HttpUtility.UrlEncode(category)}";
                     }
         
                     var data = await _http.GetStringAsync(url);
                     SetCache(cacheKey, data);
                     return data;
                 }

        public async Task<IEnumerable<string>> GetSelCategoriesAsync()
        {
            var cacheKey = "sellingCategories";
            var cachedData = GetFromCache<IEnumerable<string>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<string>>("https://localhost:7279/api/productattributes/selling-categories");
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

        public async Task<IEnumerable<ReferenceInfo>> GetAllReferenceInfo()
        {
            var cacheKey = "referenceInfo";
            var cachedData = GetFromCache<IEnumerable<ReferenceInfo>>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<IEnumerable<ReferenceInfo>>("https://localhost:7279/api/referenceinfo");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<ReferenceInfo> GetReferenceInfoByTopic(string topic)
        {
            var cacheKey = $"referenceInfo_{topic}";
            var cachedData = GetFromCache<ReferenceInfo>(cacheKey);
            if (cachedData != null) return cachedData;

            var data = await _http.GetFromJsonAsync<ReferenceInfo>($"https://localhost:7279/api/referenceinfo/{topic}");
            SetCache(cacheKey, data);
            return data;
        }

        public async Task<IEnumerable<ProductDto>> GetFilteredProducts(ProductFilter filter)
        {
            var queryString = filter.ToQueryString();
            var url = $"https://localhost:7279/api/products/filter?{queryString}";
            return await _http.GetFromJsonAsync<IEnumerable<ProductDto>>(url);
        }
    }
}
