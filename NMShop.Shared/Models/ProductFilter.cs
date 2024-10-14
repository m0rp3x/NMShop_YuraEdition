using System;
using System.Collections.Generic;
using System.Linq;

namespace NMShop.Shared.Models
{
    public class ProductFilter : IEquatable<ProductFilter>
    {
        public List<string> Brands { get; set; } = new List<string>();
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Gender { get; set; }
        public string? Category { get; set; }
        public bool InStock { get; set; } = false;
        public string? Color { get; set; }
        public List<string> SubCategories { get; set; } = new List<string>();
        public string? SelCategory { get; set; }
        public string? SortBy { get; set; }
        public bool IsAscending { get; set; } = false;
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string SearchQuery { get; set; } = string.Empty;
        public List<decimal> Sizes { get; set; } = new ();
        
        public decimal? MinSize { get; set; }
        public decimal? MaxSize { get; set; }

        public bool Equals(ProductFilter other)
        {
            if (other == null)
                return false;

            return
                this.MinPrice == other.MinPrice &&
                this.MaxPrice == other.MaxPrice &&
                this.MinSize == other.MinSize &&
                this.MaxSize == other.MaxSize &&
                this.Gender == other.Gender &&
                this.Category == other.Category &&
                this.InStock == other.InStock &&
                this.Color == other.Color &&
                this.SelCategory == other.SelCategory &&
                this.SortBy == other.SortBy &&
                this.IsAscending == other.IsAscending &&
                this.Skip == other.Skip &&
                this.Take == other.Take &&
                this.SearchQuery == other.SearchQuery &&
                this.Brands.SequenceEqual(other.Brands) &&
                this.SubCategories.SequenceEqual(other.SubCategories) &&
                this.Sizes.SequenceEqual(other.Sizes);
       
        }

        public override bool Equals(object obj) => Equals(obj as ProductFilter);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(MinPrice);
            hashCode.Add(MaxPrice);
            hashCode.Add(Gender);
            hashCode.Add(Category);
            hashCode.Add(InStock);
            hashCode.Add(Color);
            hashCode.Add(SelCategory);
            hashCode.Add(SortBy);
            hashCode.Add(IsAscending);
            hashCode.Add(Skip);
            hashCode.Add(Take);
            hashCode.Add(SearchQuery);
            hashCode.Add(MinSize);
            hashCode.Add(MaxSize);
            

            foreach (var brand in Brands)
                hashCode.Add(brand);

            foreach (var subCategory in SubCategories)
                hashCode.Add(subCategory);

            foreach (var size in Sizes)
                hashCode.Add(size);

            return hashCode.ToHashCode();
        }

        public ProductFilter Clone()
        {
            return new ProductFilter
            {
                Brands = new List<string>(this.Brands),
                MinPrice = this.MinPrice,
                MaxPrice = this.MaxPrice,
                Gender = this.Gender,
                Category = this.Category,
                InStock = this.InStock,
                Color = this.Color,
                SubCategories = new List<string>(this.SubCategories),
                SelCategory = this.SelCategory,
                SortBy = this.SortBy,
                IsAscending = this.IsAscending,
                Skip = this.Skip,
                Take = this.Take,
                SearchQuery = this.SearchQuery,
                MinSize = this.MinSize,
                MaxSize = this.MaxSize,
                Sizes = new List<decimal>(this.Sizes)
            };
        }
        
    }
}
