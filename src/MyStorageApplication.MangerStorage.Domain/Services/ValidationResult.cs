namespace MyStorageApplication.StorageManager.Domain.Services
{
    public class ValidationResult
    {
        public List<string> ValidationMessageResult { get; private set; } = new List<string>();
        public bool IsSuccess => ValidationMessageResult.Count != 0;

        public void AddMessageResult(string message)
        {
            ValidationMessageResult.Add(message);
        }
    }
}
