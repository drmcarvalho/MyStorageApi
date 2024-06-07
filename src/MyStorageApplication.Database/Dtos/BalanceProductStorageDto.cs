namespace MyStorageApplication.Database.Dtos
{
    public class BalanceProductStorageDto
    {
        public int BalanceProductStorageId { get; set; }
        public int ProductId { get; set; }
        public int StorageId { get; set; }
        public int Balance { get; set; }
    }
}
