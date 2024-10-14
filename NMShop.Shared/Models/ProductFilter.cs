namespace NMShop.Shared.Models
{
    public class ProductFilter : IEquatable<ProductFilter>
    {
        public List<int>? BrandIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<int>? GenderIds { get; set; }
        public int? CategoryId { get; set; }
        public bool InStock { get; set; } = false;
        public List<int>? ColorIds { get; set; }
        public List<int>? SubCategoryIds { get; set; }
        public List<int>? SelCategoryIds { get; set; }
        public string? SortBy { get; set; }
        public bool IsAscending { get; set; } = false;
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string SearchQuery { get; set; } = string.Empty;
        public List<decimal> Sizes { get; set; } = new();
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
                ListsAreEqual(this.GenderIds, other.GenderIds) &&
                this.CategoryId == other.CategoryId &&
                this.InStock == other.InStock &&
                ListsAreEqual(this.ColorIds, other.ColorIds) &&
                ListsAreEqual(this.SelCategoryIds, other.SelCategoryIds) &&
                this.SortBy == other.SortBy &&
                this.IsAscending == other.IsAscending &&
                this.Skip == other.Skip &&
                this.Take == other.Take &&
                this.SearchQuery == other.SearchQuery &&
                ListsAreEqual(this.BrandIds, other.BrandIds) &&
                ListsAreEqual(this.SubCategoryIds, other.SubCategoryIds) &&
                ListsAreEqual(this.Sizes, other.Sizes);
        }

        private bool ListsAreEqual<T>(List<T>? list1, List<T>? list2)
        {
            if (list1 == null && list2 == null)
                return true;

            if (list1 == null || list2 == null)
                return false;

            return list1.SequenceEqual(list2);
        }

        public override bool Equals(object obj) => Equals(obj as ProductFilter);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(MinPrice);
            hashCode.Add(MaxPrice);
            if (GenderIds != null)
                foreach (var genderId in GenderIds)
                    hashCode.Add(genderId);
            hashCode.Add(CategoryId);
            hashCode.Add(InStock);
            if (ColorIds != null)
                foreach (var colorId in ColorIds)
                    hashCode.Add(colorId);
            if (SelCategoryIds != null)
                foreach (var selCategoryId in SelCategoryIds)
                    hashCode.Add(selCategoryId);
            if (BrandIds != null)
                foreach (var brandId in BrandIds)
                    hashCode.Add(brandId);
            hashCode.Add(SortBy);
            hashCode.Add(IsAscending);
            hashCode.Add(Skip);
            hashCode.Add(Take);
            hashCode.Add(SearchQuery);
            hashCode.Add(MinSize);
            hashCode.Add(MaxSize);
            if (SubCategoryIds != null)
                foreach (var subCategoryId in SubCategoryIds)
                    hashCode.Add(subCategoryId);
            foreach (var size in Sizes)
                hashCode.Add(size);

            return hashCode.ToHashCode();
        }

        public ProductFilter Clone()
        {
            return new ProductFilter
            {
                BrandIds = this.BrandIds != null ? new List<int>(this.BrandIds) : null,
                MinPrice = this.MinPrice,
                MaxPrice = this.MaxPrice,
                GenderIds = this.GenderIds != null ? new List<int>(this.GenderIds) : null,
                CategoryId = this.CategoryId,
                InStock = this.InStock,
                ColorIds = this.ColorIds != null ? new List<int>(this.ColorIds) : null,
                SubCategoryIds = this.SubCategoryIds != null ? new List<int>(this.SubCategoryIds) : null,
                SelCategoryIds = this.SelCategoryIds != null ? new List<int>(this.SelCategoryIds) : null,
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