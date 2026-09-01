namespace EcommerceDB.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string StockText { get; set; } = string.Empty;
        public string Badge { get; set; } = string.Empty;
    }
}
