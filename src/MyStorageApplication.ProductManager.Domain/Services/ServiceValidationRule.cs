using MyStorageApplication.ProductManager.Domain.Helpers;

namespace MyStorageApplication.ProductManager.Domain.Services
{
    public class ServiceValidationRule: ServiceMessageValidation
    {
        public void CheckRuleForEmptyField<T>(T dto, string fieldName)
        {
            var val = typeof(T)?.GetProperty(fieldName)?.GetValue(dto)?.ToString();
            if (string.IsNullOrWhiteSpace(val))
            {
                ValidationResult.AddMessageResult(string.Format(MessagesHelper.MESSAGE_FIELD_REQUERID, fieldName));
            }
        }
    }
}
