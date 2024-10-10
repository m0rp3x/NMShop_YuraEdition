using NMShop.Shared.Models;
using System.Net.Http.Json;

namespace NMShop.Client.Data
{
    public class ClientTestDataProvider
    {
        private readonly HttpClient _http;

        public ClientTestDataProvider(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<string>> GetBrandsAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<string>>("https://localhost:7279/api/productattributes/brands");
        }

        public async Task<IEnumerable<string>> GetGendersAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<string>>("https://localhost:7279/api/productattributes/genders");
        }

        public async Task<IEnumerable<string>> GetSubCategoriesAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<string>>("https://localhost:7279/api/productattributes/product-types");
        }

        public async Task<IEnumerable<string>> GetSelCategoriesAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<string>>("https://localhost:7279/api/productattributes/selling-categories");
        }

        public async Task<IEnumerable<ProductDto>> GetAll() => await _http.GetFromJsonAsync<IEnumerable<ProductDto>>("https://localhost:7279/api/products");
        public async Task<ProductDto> GetById(int id) => await _http.GetFromJsonAsync<ProductDto>($"https://localhost:7279/api/products/id/{id}");

        // Дополнительные методы для доступа к данным
        public async Task<IEnumerable<ProductDto>> GetShoes() => await _http.GetFromJsonAsync<IEnumerable<ProductDto>>("https://localhost:7279/api/products/category/Обувь");
        public async Task<IEnumerable<ProductDto>> GetClothes() => await _http.GetFromJsonAsync<IEnumerable<ProductDto>>("https://localhost:7279/api/products/category/Одежда");
        public async Task<IEnumerable<ProductDto>> GetAccessories() => await _http.GetFromJsonAsync<IEnumerable<ProductDto>>("https://localhost:7279/api/products/category/Аксессуары");

        public async Task<IEnumerable<ReferenceInfo>> GetAllReferenceInfo() => await _http.GetFromJsonAsync<IEnumerable<ReferenceInfo>>("https://localhost:7279/api/referenceinfo");
        public async Task<ReferenceInfo> GetReferenceInfoByTopic(string topic) => await _http.GetFromJsonAsync<ReferenceInfo>($"https://localhost:7279/api/referenceinfo/{topic}");
    }
}