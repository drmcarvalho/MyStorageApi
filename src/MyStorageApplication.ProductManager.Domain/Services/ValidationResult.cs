namespace MyStorageApplication.ProductManager.Domain.Services
{
    public class ValidationResult
    {
        public List<string> ValidationMessageResult { get; private set; } = [];
        public bool IsSuccess => ValidationMessageResult.Count == 0;

        public void AddMessageResult(string message)
        {
            ValidationMessageResult.Add(message);
        }
    }
}
