namespace MyStorageApplication.ProductManager.Domain.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public int StockBalance { get; set; }
        public decimal Price { get; set; }
    }
}
