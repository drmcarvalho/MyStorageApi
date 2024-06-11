namespace MyStorageApplication.Database.Dtos
{
    public class StorageDto
    {
        public int StorageId {  get; set; }
        public string? Identification { get; set; }
        public int BalanceTotal { get; set; } = 0;
    }
}
