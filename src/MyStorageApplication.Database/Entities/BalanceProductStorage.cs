namespace MyStorageApplication.Database.Entities
{
    public class BalanceProductStorage(int productId, int storageId, int balance)
    {
        public int BalanceProductStorageId { get; private set; }
        public int Balance { get; private set; } = balance;
        public int ProductId { get; private set; } = productId;
        public int StorageId { get; private set; } = storageId;
    }
}
