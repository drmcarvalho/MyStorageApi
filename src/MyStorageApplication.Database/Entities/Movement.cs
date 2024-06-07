namespace MyStorageApplication.Database.Entities
{
    public class Movement(int amount, int productId, int storageId, string type, string productName)
    {
        public int MovementId { get; private set; }
        public int Amount { get; private set; } = amount;
        public int ProductId { get; private set; } = productId;
        public string ProductName { get; private set; } = productName;
        public int StorageId { get; private set; } = storageId;
        public string Type { get; private set; } = type;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
    }
}
