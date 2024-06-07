using MyStorageApplication.StorageManager.Domain.Dtos;
using MyStorageApplication.StorageManager.Domain.Helpers;

namespace MyStorageApplication.StorageManager.Domain.Services
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

        public void CheckRuleForNumberIsZero<T>(T dto, string fieldName)
        {
            var val = typeof(T)?.GetProperty(fieldName)?.GetValue(dto)?.ToString();            
            if (int.TryParse(val, out int result) && result <= 0)
            {
                ValidationResult.AddMessageResult(string.Format(MessagesHelper.MESSAGE_NUMBER_VALUE_REQUERID, fieldName));
            }
        }

        public void CheckRuleForTypeMovement(string type)
        {            
            if (string.IsNullOrWhiteSpace(type) || !(type.Equals("S") || type.Equals("E")))
            {
                ValidationResult.AddMessageResult(string.Format(MessagesHelper.MESSAGE_INVALID_TYPE_OF_MOVEMENTATION));
            }
        }
    }
}
