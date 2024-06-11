using MyStorageApplication.Database.Dtos;
using MyStorageApplication.Database.Entities;
using MyStorageApplication.Database.Repositories.Interfaces;
using MyStorageApplication.Database.UoW;
using MyStorageApplication.StorageManager.Domain.Dtos;
using MyStorageApplication.StorageManager.Domain.Helpers;
using MyStorageApplication.StorageManager.Domain.Services.Interfaces;

namespace MyStorageApplication.StorageManager.Domain.Services
{
    public class StorageManagerServiceDomain(
        IStorageReadOnlyRepository storageReadOnlyRepository, 
        IStorageWriteOnlyRepository storageWriteOnlyRepository,
        IMovementsReadOnlyRepository movementsReadOnlyRepository,
        IMovementsWriteOnlyRepository movementsWriteOnlyRepository,
        IProductReadOnlyRepository productReadOnlyRepository,
        IProductWriteOnlyRepository productWriteOnlyRepository,
        IBalanceProductStorageReadOnlyRepository balanceProductStorageReadOnlyRepository,
        IBalanceProductStorageWriteOnlyRepository balanceProductStorageWriteOnlyRepository,
        IUnitOfWork unitOfWork): ServiceValidationRule, IStorageManagerServiceDomain
    {
        private readonly IStorageReadOnlyRepository _storageReadOnlyRepository = storageReadOnlyRepository;
        private readonly IStorageWriteOnlyRepository _storageWriteOnlyRepository = storageWriteOnlyRepository;
        private readonly IMovementsReadOnlyRepository _movementsReadOnlyRepository = movementsReadOnlyRepository;
        private readonly IMovementsWriteOnlyRepository _movementsWriteOnlyRepository = movementsWriteOnlyRepository;
        private readonly IProductReadOnlyRepository _productReadOnlyRepository = productReadOnlyRepository;
        private readonly IProductWriteOnlyRepository _productWriteOnlyRepository = productWriteOnlyRepository;
        private readonly IBalanceProductStorageReadOnlyRepository _balanceReadOnlyRepository = balanceProductStorageReadOnlyRepository;
        private readonly IBalanceProductStorageWriteOnlyRepository _balanceWriteOnlyRepository = balanceProductStorageWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ValidationResult> CreateStorageAsync(CreateStorageDto createStorageDto)
        {           
            CheckRuleForEmptyField(createStorageDto, nameof(createStorageDto.Identification));

            if (!ValidationResult.IsSuccess)
            {
                return ValidationResult;
            }
            
            await _storageWriteOnlyRepository.InsertAsync(new Storage(createStorageDto.Identification));            

            return ValidationResult;
        }

        public async Task<ValidationResult> UpdateStorageAsync(UpdateStorageDto updateStorageDto)
        {
            var storage = await _storageReadOnlyRepository.GetByIdAsync(updateStorageDto.Id);
            if (storage is null)
            {
                ValidationResult.AddMessageResult(string.Format(MessagesHelper.MESSAGE_NOT_FOUND, "Estoque"));                
            }

            CheckRuleForEmptyField(updateStorageDto, nameof(updateStorageDto.Identification));

            if (!ValidationResult.IsSuccess)
            {
                return ValidationResult;
            }

            var storageForUpdate = new Storage();
            storageForUpdate.Update(updateStorageDto.Id, updateStorageDto.Identification);
            await _storageWriteOnlyRepository.UpdateAsync(storageForUpdate);

            return ValidationResult;
        }

        public async Task<ValidationResult> RegisterMovementInStorage(RegisterMovementInStorageDto registerMovemenDto)
        {
            CheckRuleForEmptyField(registerMovemenDto, nameof(registerMovemenDto.Type));
            CheckRuleForNumberIsZero(registerMovemenDto, nameof(registerMovemenDto.Amount));
            CheckRuleForTypeMovement(registerMovemenDto.Type);

            if (!ValidationResult.IsSuccess)
            {
                return ValidationResult;
            }

            var typeMovement = registerMovemenDto.Type;

            var storageDto = await _storageReadOnlyRepository.GetByIdAsync(registerMovemenDto.StorageId);
            
            var productDto = await _productReadOnlyRepository.GetByIdAsync(registerMovemenDto.ProductId);

            if (storageDto is null)
            {
                ValidationResult.AddMessageResult(string.Format(MessagesHelper.MESSAGE_NOT_FOUND, "Estoque"));                
            }            
            
            if (productDto is null)
            {
                ValidationResult.AddMessageResult(string.Format(MessagesHelper.MESSAGE_NOT_FOUND, "Produto"));                
            }

            if (!ValidationResult.IsSuccess)
            {
                return ValidationResult;
            }

            if (typeMovement.Equals("S"))
            {
                if (registerMovemenDto.Amount > productDto?.StockBalance)
                {
                    ValidationResult.AddMessageResult(string.Format(MessagesHelper.MESSAGE_INSUFFICIENT_STOCK_BALANCE));
                }
            }
            
            var balanceProductStorageDto = await _balanceReadOnlyRepository.GetByIdAsync(registerMovemenDto.ProductId, storageDto!.StorageId);
            if (typeMovement.Equals("S") && balanceProductStorageDto is not null)
            {
                if (registerMovemenDto.Amount > balanceProductStorageDto.Balance)
                {
                    ValidationResult.AddMessageResult(string.Format(MessagesHelper.MESSAGE_INSUFFICIENT_STOCK_BALANCE_SELECTED));
                }
            }

            if (!ValidationResult.IsSuccess)
            {
                return ValidationResult;
            }

            _unitOfWork.BeginTransaction(); 
            {
                try
                {
                    var registerNewMovementation = new Movement(
                        registerMovemenDto.Amount,
                        registerMovemenDto.ProductId,
                        registerMovemenDto.StorageId,
                        registerMovemenDto.Type, productDto!.ProductName);

                    
                    if (balanceProductStorageDto is null)
                    {
                        await _balanceWriteOnlyRepository.InsertAsync(new BalanceProductStorage(productDto.ProductId, storageDto.StorageId, registerMovemenDto.Amount));
                    }
                    else
                    {                        
                        balanceProductStorageDto.Balance = CalculateStockBalance(typeMovement, balanceProductStorageDto.Balance, registerMovemenDto.Amount);
                        await _balanceWriteOnlyRepository.UpdateBalanceAsync(balanceProductStorageDto); 
                    }

                    var stockBalanceCalculated = CalculateStockBalance(typeMovement, productDto.StockBalance, registerMovemenDto.Amount);

                    await _productWriteOnlyRepository.UpdateStokBalanceAsync(stockBalanceCalculated, productDto.ProductId);

                    await _movementsWriteOnlyRepository.InsertAsync(registerNewMovementation);

                    _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Commit exception type: {ex.GetType()}");
                    Console.WriteLine($"\nMessage: {ex.Message}");

                    ValidationResult.AddMessageResult("Commit fail");

                    try
                    {
                        _unitOfWork.Rollback();
                    }
                    catch (Exception ex2) 
                    {
                        Console.WriteLine($"Rollback exception type: {ex2.GetType()}");
                        Console.WriteLine($"\nMessage: {ex2.Message}");

                        ValidationResult.AddMessageResult("Rollback");
                    }
                }
            }            

            return ValidationResult;
        }

        public async Task<IEnumerable<StorageDto>> GetAllAsync()
            => await _storageReadOnlyRepository.GetAllAsync();

        public async Task<StorageDto?> GetByIdAsync(int id)         
            => await _storageReadOnlyRepository.GetByIdAsync(id);

        public async Task<IEnumerable<StorageDto>> QueryStorage(string query)
            => await _storageReadOnlyRepository.QueryAsync(query);

        public async Task<IEnumerable<HistoryMovementDto>> GetAllHistoryMovimentsAsync()
        { 
            var historyList = await _movementsReadOnlyRepository.GetAllAsync();
            foreach (var historyMovementDto in historyList)
            {
                historyMovementDto.Type = historyMovementDto.Type.Equals("E") ? "Entrada" : "Saída";
            }
            return historyList;
        }

        private static int CalculateStockBalance(string type, int currentAmount, int newAmount)
        {
            if (type.Equals("S"))
            {
                currentAmount -= newAmount;
            }
            else if (type.Equals("E"))
            {
                currentAmount += newAmount;
            }

            return currentAmount;
        }
    }
}
