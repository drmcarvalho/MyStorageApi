namespace MyStorageApplication.Database.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int StockBalance { get; set; }
        public decimal Price { get; set; }
    }
}
