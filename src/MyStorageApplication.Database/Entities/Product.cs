namespace MyStorageApplication.Database.Entities
{
    public class Product
    {
        public Product() { }

        public int ProductId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int StockBalance { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, int stockBalance, decimal price)
        {
            Name = name;
            StockBalance = stockBalance;
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
