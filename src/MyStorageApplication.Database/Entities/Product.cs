namespace MyStorageApplication.Database.Entities
{
    public class Product
    {
        public Product() { }

        public int ProductId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int StockBalance { get; private set; } = 0;
        public decimal Price { get; private set; }
        public int Deleted { get; private set; } = 0;

        public Product(string name, decimal price)
        {
            Name = name;            
            Price = price;
        }

        public void Update(int productId, string name, decimal price)
        {
            ProductId = productId;
            Name = name;            
            Price = price;
        }
    }
}
