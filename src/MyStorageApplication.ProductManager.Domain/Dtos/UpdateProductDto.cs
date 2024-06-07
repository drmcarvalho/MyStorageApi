namespace MyStorageApplication.ProductManager.Domain.Dtos
{
    public class UpdateProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;        
        public decimal Price { get; set; }
    }
}
