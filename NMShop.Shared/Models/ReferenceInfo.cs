namespace NMShop.Shared.Models
{

    public class ReferenceInfo
    {
        public string Topic { get; set; }
        public List<Content> Contents { get; set; } = new List<Content>();
    }

    public class Content
    {
        public string Size { get; set; }
        public string Text { get; set; }
        public bool IsBold { get; set; }
    }
}
