namespace MyStorageApplication.Database.Dtos
{
    public class HistoryMovementDto
    {
        public int MovementId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string StorageName {  get; set; } = string.Empty;
        public int Amount { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
