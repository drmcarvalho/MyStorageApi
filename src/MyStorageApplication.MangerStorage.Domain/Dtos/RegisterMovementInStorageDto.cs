namespace MyStorageApplication.StorageManager.Domain.Dtos
{
    public class RegisterMovementInStorageDto
    {
        public int ProductId { get; set; }
        public int StorageId { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Amount { get; set; } = 0;
    }
}
