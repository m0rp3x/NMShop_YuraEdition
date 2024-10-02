// Server/Data/TestDataProvider.cs
using NMShop.Shared.Models;
using System.Collections.Generic;

namespace NMShop.Data
{
    public static class TestDataProvider
    {
        public static List<Product> GetTestProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Кроссовки Nike Air Max",
                    Brand = "Nike",
                    Article = "NKAM001",
                    Description = "Легкие кроссовки для бега.",
                    Gender = "male",
                    SubCategory = "Кеды и Кроссовки",
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
                    Id = 2,
                    Name = "Кроссовки Puma RS-X",
                    Brand = "Puma",
                    Article = "PUMRS001",
                    Description = "Стильные и удобные кроссовки для повседневной носки.",
                    Gender = "male",
                    SubCategory = "Кеды и Кроссовки",
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
                    Id = 3,
                    Name = "Кроссовки Adidas Ultraboost",
                    Brand = "Adidas",
                    Article = "ADUB001",
                    Description = "Беговые кроссовки с амортизацией Boost.",
                    Gender = "female",
                    SubCategory = "Кеды и Кроссовки",
                    ProductType = "shoes",
                    Color = new Dictionary<string, string>
                    {
                        { "Синий", "#0000FF" }
                    },
                    PriceInfos = new List<PriceInfo>
                    {
                        new PriceInfo { Size = 38, Price = 140m, DiscountPrice = 120m, Stock = 0 }
                    }
                },
                new Product
                {
                    Id = 4,
                    Name = "Кроссовки Reebok Classic",
                    Brand = "Reebok",
                    Article = "RBKCL001",
                    Description = "Классические кожаные кроссовки.",
                    Gender = "unisex",
                    SubCategory = "Кеды и Кроссовки",
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
                    Id = 5,
                    Name = "Футболка Adidas",
                    Brand = "Adidas",
                    Article = "ADTS001",
                    Description = "Классическая футболка из хлопка.",
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
                    Id = 6,
                    Name = "Футболка Nike Dri-FIT",
                    Brand = "Nike",
                    Article = "NKTS002",
                    Description = "Спортивная футболка для тренировок с технологией Dri-FIT.",
                    Gender = "female",
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
                    Id = 7,
                    Name = "Футболка Under Armour",
                    Brand = "Under Armour",
                    Article = "UATS001",
                    Description = "Комфортная футболка для тренировок.",
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
                    Id = 8,
                    Name = "Футболка Puma Essentials",
                    Brand = "Puma",
                    Article = "PUMTS001",
                    Description = "Базовая футболка с логотипом Puma.",
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
                    Id = 9,
                    Name = "Кепка Nike",
                    Brand = "Nike",
                    Article = "NKCP001",
                    Description = "Спортивная кепка с логотипом Nike.",
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
                },
                new Product
                {
                    Id = 13,
                    Name = "Ботинки Timberland",
                    Brand = "Timberland",
                    Article = "TMBL001",
                    Description = "Водонепроницаемые ботинки для холодной погоды.",
                    Gender = "male",
                    SubCategory = "Ботинки и Угги",
                    ProductType = "shoes",
                    Color = new Dictionary<string, string>
                    {
                        { "Коричневый", "#A52A2A" },
                        { "Черный", "#000000" }
                    },
                    PriceInfos = new List<PriceInfo>
                    {
                        new PriceInfo { Size = 43, Price = 150m, DiscountPrice = 130m, Stock = 12 }
                    }
                },
                new Product
                {
                    Id = 14,
                    Name = "Угги UGG Classic",
                    Brand = "UGG",
                    Article = "UGG001",
                    Description = "Угги с мягким внутренним мехом.",
                    Gender = "female",
                    SubCategory = "Ботинки и Угги",
                    ProductType = "shoes",
                    Color = new Dictionary<string, string>
                    {
                        { "Бежевый", "#F5F5DC" },
                        { "Серый", "#808080" }
                    },
                    PriceInfos = new List<PriceInfo>
                    {
                        new PriceInfo { Size = 38, Price = 180m, DiscountPrice = 160m, Stock = 5 }
                    }
                },
                new Product
                {
                    Id = 15,
                    Name = "Слайды Adidas Adilette",
                    Brand = "Adidas",
                    Article = "ADSL001",
                    Description = "Комфортные слайды для пляжа и бассейна.",
                    Gender = "unisex",
                    SubCategory = "Слайды",
                    ProductType = "shoes",
                    Color = new Dictionary<string, string>
                    {
                        { "Синий", "#0000FF" },
                        { "Белый", "#FFFFFF" }
                    },
                    PriceInfos = new List<PriceInfo>
                    {
                        new PriceInfo { Size = 42, Price = 40m, DiscountPrice = null, Stock = 20 }
                    }
                },
                new Product
                {
                    Id = 16,
                    Name = "Детские кеды Converse",
                    Brand = "Converse",
                    Article = "CNVKD001",
                    Description = "Легкие детские кеды для активного отдыха.",
                    Gender = "unisex",
                    SubCategory = "Детское",
                    ProductType = "shoes",
                    Color = new Dictionary<string, string>
                    {
                        { "Красный", "#FF0000" },
                        { "Белый", "#FFFFFF" }
                    },
                    PriceInfos = new List<PriceInfo>
                    {
                        new PriceInfo { Size = 30, Price = 50m, DiscountPrice = 45m, Stock = 15 }
                    }
                }
            };
        }
    }
}