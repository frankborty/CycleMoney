namespace CycleMoney.API.Services
{
    public class RecurringExpenseService : IRecurringExpenseService
    {
        private readonly CycleMoneyRemoteDbContext _dbContext;

        public RecurringExpenseService(CycleMoneyRemoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<Result<List<ExpenseDto>>> GetAllAsync()
        {
            try
            {
                var data = await _dbContext.Expenses
                    .Include(r => r.RecurrenceType)
                    .Include(r => r.PaymentType)
                    .ToListAsync();
                
                var result = data.Select(r => ExpenseConverter.ToDto(r)).ToList();

                return Result<List<ExpenseDto>>.Ok(result);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException is not null)
                {
                    errorMsg += " - InnerExc: " + ex.InnerException;
                }
                return Result<List<ExpenseDto>>.Fail(errorMsg);
            }
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<Result<ExpenseDto>> GetByIdAsync(int id)
        {
            var expense = await _dbContext.Expenses
                .Include(r => r.RecurrenceType)
                .Include(r => r.PaymentType)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (expense is null)
            {
                return Result<ExpenseDto>.Fail("Spesa non trovata");
            }

            return Result<ExpenseDto>.Ok(ExpenseConverter.ToDto(expense));
        }

        // =========================
        // CREATE
        // =========================
        public async Task<Result<ExpenseDto>> CreateAsync(ExpenseDto dto)
        {
            try
            {
                dto.Id = 0;
                if (dto.Amount <= 0)
                {
                    return Result<ExpenseDto>.Fail("Amount must be greater than zero");
                }

                var recurrenceType = await _dbContext.RecurrenceTypes
                    .FirstOrDefaultAsync(r => r.Id == dto.RecurrenceTypeId);

                if (recurrenceType == null)
                {
                    return Result<ExpenseDto>.Fail("Invalid recurrence type");
                }

                var paymentType = await _dbContext.PaymentTypes
                    .FirstOrDefaultAsync(r => r.Id == dto.PaymentTypeId);

                if (paymentType == null)
                {
                    return Result<ExpenseDto>.Fail("Invalid payment type");
                }

                var entity = ExpenseConverter.ToEntity(dto);

                _dbContext.Expenses.Add(entity);
                await _dbContext.SaveChangesAsync();

                dto.Id = entity.Id;
                return Result<ExpenseDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if(ex.InnerException is not null)
                {
                    errorMsg += " - InnerExc: " + ex.InnerException;
                }
                return Result<ExpenseDto>.Fail(errorMsg);
            }
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<Result<ExpenseDto>> UpdateAsync(int id, ExpenseDto dto)
        {
            try
            {
                var entity = await _dbContext.Expenses.FindAsync(id);

                if (entity == null)
                {
                    return Result<ExpenseDto>.Fail("Recurring expense not found");
                }

                // Validazione base
                if (dto.Amount <= 0)
                {
                    return Result<ExpenseDto>.Fail("Amount must be greater than zero");
                }

                // Controllo RecurrenceType
                var recurrenceTypeExists = await _dbContext.RecurrenceTypes
                    .AnyAsync(r => r.Id == dto.RecurrenceTypeId);

                if (!recurrenceTypeExists)
                {
                    return Result<ExpenseDto>.Fail("Invalid recurrence type");
                }

                // Controllo PaymentType
                var paymentTypeExists = await _dbContext.PaymentTypes
                    .AnyAsync(p => p.Id == dto.PaymentTypeId);

                if (!paymentTypeExists)
                {
                    return Result<ExpenseDto>.Fail("Invalid payment type");
                }

                // UPDATE FIELDS
                entity.RecurrenceTypeId = dto.RecurrenceTypeId;
                entity.PaymentTypeId = dto.PaymentTypeId;
                entity.Date = dto.Date;
                entity.Automatic = dto.Automatic;
                entity.Amount = dto.Amount;
                entity.Description = dto.Description;

                await _dbContext.SaveChangesAsync();

                // ritorno DTO aggiornato
                return Result<ExpenseDto>.Ok(new ExpenseDto
                {
                    Id = entity.Id,
                    Amount = entity.Amount,
                    Description = entity.Description,
                    RecurrenceTypeId = entity.RecurrenceTypeId,
                    PaymentTypeId = entity.PaymentTypeId,
                    Date = entity.Date,
                    Automatic = entity.Automatic
                });
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException is not null)
                {
                    errorMsg += " - InnerExc: " + ex.InnerException;
                }
                return Result<ExpenseDto>.Fail(errorMsg);
            }
        }


        // =========================
        // DELETE
        // =========================
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbContext.Expenses.FindAsync(id);

                if (entity == null)
                {
                    return Result<bool>.Fail("Recurring expense not found");
                }

                _dbContext.Expenses.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException is not null)
                {
                    errorMsg += " - InnerExc: " + ex.InnerException;
                }
                return Result<bool>.Fail($"Error deleting recurring expense: {errorMsg}");
            }
        }
    }
}
