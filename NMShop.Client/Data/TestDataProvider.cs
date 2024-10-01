using NMShop.Shared.Models;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace NMShop.Client.Data
{
    public class TestDataProvider
    {
        private readonly HttpClient _http;

        public TestDataProvider(HttpClient http)
        {
            _http = http;
        }

        public static string[] GetTestBrands()
        {
            return new string[]
            {
                "Nike",
                "Adidas",
                "Puma",
                "Reebok",
                "Under Armour",
                "New Balance",
                "Converse",
                "Vans",
                "Gucci",
                "Louis Vuitton",
                "Prada",
                "Versace",
                "Balenciaga",
                "H&M",
                "Zara",
                "Uniqlo",
                "Levi's",
                "Calvin Klein",
                "Tommy Hilfiger",
                "Ralph Lauren"
            };
        }


        public static string[] GetTestGenders()
        {
            return new string[]
            {
                "Унисекс",
                "Мужской",
                "Женский",
            };
        }
        public static bool[] GetStockFilterOptions()
        {
            return new bool[]
            {
                true, // В наличии
                false // Нет в наличии
            };
        }




        public async Task<IEnumerable<Product>> GetAll() => await _http.GetFromJsonAsync<IEnumerable<Product>>("https://localhost:7279/api/products");
        public async Task<Product> GetById(int id) => await _http.GetFromJsonAsync<Product>($"https://localhost:7279/api/products/id/{id}");
        // Дополнительные методы для доступа к данным
        public async Task<IEnumerable<Product>> GetShoes() => await _http.GetFromJsonAsync<IEnumerable<Product>>("https://localhost:7279/api/products/shoes");
        public async Task<IEnumerable<Product>> GetClothes() => await _http.GetFromJsonAsync<IEnumerable<Product>>("https://localhost:7279/api/products/clothes");
        public async Task<IEnumerable<Product>> GetAccessories() => await _http.GetFromJsonAsync<IEnumerable<Product>>("https://localhost:7279/api/products/accessories");
    }
}
