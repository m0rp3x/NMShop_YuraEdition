using NMShop.Shared.Models;
namespace NMShop.Client.Data
{
    public static class TestDataProvider
    {

        public static string[] GetTestBrands()
        {
            string[] brands = new string[]
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
            return brands;
        }

        public static IEnumerable<Product> ShoesPreview = TestDataProvider.GetTestProducts().Where(p => p.ProductType == "shoes").Take(4);
        public static IEnumerable<Product> ClothesPreview = TestDataProvider.GetTestProducts().Where(p => p.ProductType == "clothes").Take(4);
        public static IEnumerable<Product> AccessoriesPreview = TestDataProvider.GetTestProducts().Where(p => p.ProductType == "accessories").Take(4);
        public static List<Product> GetTestProducts()
        {
            return new List<Product>
    {
        // Спортивная обувь
        new Product
        {
            Id = 1,
            Name = "Кроссовки Nike Air Max",
            Brand = "Nike",
            Article = "NKAM001",
            Description = "Легкие кроссовки для бега.",
            ImageUrl = "/biba.jpg",
            Gender = "male",
            SubCategory = "Спортивная обувь",
            ProductType = "shoes",
            Color = new Dictionary<string, string>
            {
                { "Черный", "#000000" },
                { "Белый", "#FFFFFF" }
            },
            PriceInfos = new List<PriceInfo>
            {
                new PriceInfo { Size = 44, Price = 120m, DiscountPrice = 100m, Stock = 10 },
                new PriceInfo { Size = 44.5m, Price = 125m, DiscountPrice = null, Stock = 5 }
            }
        },
        new Product
        {
            Id = 4,
            Name = "Кроссовки Puma RS-X",
            Brand = "Puma",
            Article = "PUMRS001",
            Description = "Стильные и удобные кроссовки для повседневной носки.",
            ImageUrl = "/biba.jpg",
            Gender = "male",
            SubCategory = "Спортивная обувь",
            ProductType = "shoes",
            Color = new Dictionary<string, string>
            {
                { "Серый", "#808080" },
                { "Красный", "#FF0000" }
            },
            PriceInfos = new List<PriceInfo>
            {
                new PriceInfo { Size = 42, Price = 110m, DiscountPrice = null, Stock = 8 }
            }
        },
        new Product
        {
            Id = 5,
            Name = "Кроссовки Adidas Ultraboost",
            Brand = "Adidas",
            Article = "ADUB001",
            Description = "Беговые кроссовки с амортизацией Boost.",
            ImageUrl = "/bebra.jpg",
            Gender = "female",
            SubCategory = "Спортивная обувь",
            ProductType = "shoes",
            Color = new Dictionary<string, string>
            {
                { "Синий", "#0000FF" }
            },
            PriceInfos = new List<PriceInfo>
            {
                new PriceInfo { Size = 38, Price = 140m, DiscountPrice = 120m, Stock = 5 }
            }
        },
        new Product
        {
            Id = 6,
            Name = "Кроссовки Reebok Classic",
            Brand = "Reebok",
            Article = "RBKCL001",
            Description = "Классические кожаные кроссовки.",
            ImageUrl = "/boba.jpg",
            Gender = "unisex",
            SubCategory = "Спортивная обувь",
            ProductType = "shoes",
            Color = new Dictionary<string, string>
            {
                { "Белый", "#FFFFFF" }
            },
            PriceInfos = new List<PriceInfo>
            {
                new PriceInfo { Size = 40, Price = 90m, DiscountPrice = 80m, Stock = 20 }
            }
        },

        // Футболки
        new Product
        {
            Id = 2,
            Name = "Футболка Adidas",
            Brand = "Adidas",
            Article = "ADTS001",
            Description = "Классическая футболка из хлопка.",
            ImageUrl = "/bebra.jpg",
            Gender = "unisex",
            SubCategory = "Футболки",
            ProductType = "clothes",
            Color = new Dictionary<string, string>
            {
                { "Синий", "#0000FF" },
                { "Красный", "#FF0000" }
            },
            PriceInfos = new List<PriceInfo>
            {
                new PriceInfo { Size = (decimal)ClothingSize.M, Price = 30m, DiscountPrice = null, Stock = 20 },
                new PriceInfo { Size = (decimal)ClothingSize.L, Price = 32m, DiscountPrice = 25m, Stock = 15 }
            }
        },
        new Product
        {
            Id = 7,
            Name = "Футболка Nike Dri-FIT",
            Brand = "Nike",
            Article = "NKTS002",
            Description = "Спортивная футболка для тренировок с технологией Dri-FIT.",
            ImageUrl = "/biba.jpg",
            Gender = "male",
            SubCategory = "Футболки",
            ProductType = "clothes",
            Color = new Dictionary<string, string>
            {
                { "Черный", "#000000" }
            },
            PriceInfos = new List<PriceInfo>
            {
                new PriceInfo { Size = (decimal)ClothingSize.L, Price = 35m, DiscountPrice = 30m, Stock = 10 }
            }
        },
        new Product
        {
            Id = 8,
            Name = "Футболка Under Armour",
            Brand = "Under Armour",
            Article = "UATS001",
            Description = "Комфортная футболка для тренировок.",
            ImageUrl = "/bebra.jpg",
            Gender = "male",
            SubCategory = "Футболки",
            ProductType = "clothes",
            Color = new Dictionary<string, string>
            {
                { "Зеленый", "#00FF00" }
            },
            PriceInfos = new List<PriceInfo>
            {
                new PriceInfo { Size = (decimal)ClothingSize.M, Price = 28m, DiscountPrice = null, Stock = 12 }
            }
        },
        new Product
        {
            Id = 9,
            Name = "Футболка Puma Essentials",
            Brand = "Puma",
            Article = "PUMTS001",
            Description = "Базовая футболка с логотипом Puma.",
            ImageUrl = "/boba.jpg",
            Gender = "unisex",
            SubCategory = "Футболки",
            ProductType = "clothes",
            Color = new Dictionary<string, string>
            {
                { "Белый", "#FFFFFF" }
            },
            PriceInfos = new List<PriceInfo>
            {
                new PriceInfo { Size = (decimal)ClothingSize.L, Price = 25m, DiscountPrice = 20m, Stock = 25 }
            }
        },

        // Головные уборы
        new Product
        {
            Id = 3,
            Name = "Кепка Nike",
            Brand = "Nike",
            Article = "NKCP001",
            Description = "Спортивная кепка с логотипом Nike.",
            ImageUrl = "/boba.jpg",
            Gender = "unisex",
            SubCategory = "Головные уборы",
            ProductType = "accessories",
            Color = new Dictionary<string, string>
            {
                { "Черный", "#000000" }
            },
            PriceInfos = new List<PriceInfo>
            {
                new PriceInfo { Size = 1, Price = 25m, DiscountPrice = 20m, Stock = 50 }
            }
        },
        new Product
        {
            Id = 10,
            Name = "Шапка Adidas",
            Brand = "Adidas",
            Article = "ADCP002",
            Description = "Зимняя шапка с логотипом Adidas.",
            ImageUrl = "/biba.jpg",
            Gender = "unisex",
            SubCategory = "Головные уборы",
            ProductType = "accessories",
            Color = new Dictionary<string, string>
            {
                { "Серый", "#808080" }
            },
            PriceInfos = new List<PriceInfo>
            {
                new PriceInfo { Size = 1, Price = 30m, DiscountPrice = 25m, Stock = 30 }
            }
        },
        new Product
        {
            Id = 11,
            Name = "Кепка Puma",
            Brand = "Puma",
            Article = "PUMCP001",
            Description = "Классическая кепка Puma с логотипом.",
            ImageUrl = "/boba.jpg",
            Gender = "unisex",
            SubCategory = "Головные уборы",
            ProductType = "accessories",
            Color = new Dictionary<string, string>
            {
                { "Синий", "#0000FF" }
            },
            PriceInfos = new List<PriceInfo>
            {
                new PriceInfo { Size = 1, Price = 22m, DiscountPrice = null, Stock = 40 }
            }
        },
        new Product
        {
            Id = 12,
            Name = "Бейсболка Under Armour",
            Brand = "Under Armour",
            Article = "UACP001",
            Description = "Легкая бейсболка с логотипом UA.",
            ImageUrl = "/bebra.jpg",
            Gender = "unisex",
            SubCategory = "Головные уборы",
            ProductType = "accessories",
            Color = new Dictionary<string, string>
            {
                { "Красный", "#FF0000" }
            },
            PriceInfos = new List<PriceInfo>
            {
                new PriceInfo { Size = 1, Price = 27m, DiscountPrice = 22m, Stock = 35 }
            }
        }
    };
        }
        

    }
}
