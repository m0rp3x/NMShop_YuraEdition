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

        public static string[] GetTestSubCategory()
        {
            return new string[]
            {
              "Кеды и Кроссовки",
              "Ботинки и Угги",
              "Слайды",
              "Детское"
            };
        }
        public static string[] GetTestSelCategory()
        {
            return new string[]
            {
                "Новые релизы",
                "Хиты продаж",
                "Коллаборации",
                "Эксклюзивы",
                "Маст-хэв"
            };
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
