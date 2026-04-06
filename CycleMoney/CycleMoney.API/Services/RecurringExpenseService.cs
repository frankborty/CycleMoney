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
        public async Task<Result<List<RecurringExpenseDto>>> GetAllAsync()
        {
            try
            {
                var data = await _dbContext.Recurringexpenses
                    .Include(r => r.Recurrencetype)
                    .Include(r => r.Paymenttype)
                    .ToListAsync();

                var result = data.Select(r => RecurringExpenseConverter.ToDto(r)).ToList();

                return Result<List<RecurringExpenseDto>>.Ok(result);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException is not null)
                {
                    errorMsg += " - InnerExc: " + ex.InnerException;
                }
                return Result<List<RecurringExpenseDto>>.Fail(errorMsg);
            }
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<Result<RecurringExpenseDto>> GetByIdAsync(int id)
        {
            var expense = await _dbContext.Recurringexpenses
                .Include(r => r.Recurrencetype)
                .Include(r => r.Paymenttype)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (expense is null)
            {
                return Result<RecurringExpenseDto>.Fail("Spesa non trovata");
            }

            return Result<RecurringExpenseDto>.Ok(RecurringExpenseConverter.ToDto(expense));
        }

        // =========================
        // CREATE
        // =========================
        public async Task<Result<RecurringExpenseDto>> CreateAsync(RecurringExpenseDto dto)
        {
            try
            {
                dto.Id = 0;
                if (dto.Amount <= 0)
                {
                    return Result<RecurringExpenseDto>.Fail("Amount must be greater than zero");
                }

                var recurrenceType = await _dbContext.Recurrencetypes
                    .FirstOrDefaultAsync(r => r.Id == dto.RecurrenceTypeId);

                if (recurrenceType == null)
                {
                    return Result<RecurringExpenseDto>.Fail("Invalid recurrence type");
                }

                var paymentType = await _dbContext.Paymenttypes
                    .FirstOrDefaultAsync(r => r.Id == dto.PaymentTypeId);

                if (paymentType == null)
                {
                    return Result<RecurringExpenseDto>.Fail("Invalid payment type");
                }

                var entity = RecurringExpenseConverter.ToEntity(dto);

                _dbContext.Recurringexpenses.Add(entity);
                await _dbContext.SaveChangesAsync();

                dto.Id = entity.Id;
                return Result<RecurringExpenseDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if(ex.InnerException is not null)
                {
                    errorMsg += " - InnerExc: " + ex.InnerException;
                }
                return Result<RecurringExpenseDto>.Fail(errorMsg);
            }
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<Result<RecurringExpenseDto>> UpdateAsync(int id, RecurringExpenseDto dto)
        {
            try
            {
                var entity = await _dbContext.Recurringexpenses.FindAsync(id);

                if (entity == null)
                {
                    return Result<RecurringExpenseDto>.Fail("Recurring expense not found");
                }

                // Validazione base
                if (dto.Amount <= 0)
                {
                    return Result<RecurringExpenseDto>.Fail("Amount must be greater than zero");
                }

                // Controllo RecurrenceType
                var recurrenceTypeExists = await _dbContext.Recurrencetypes
                    .AnyAsync(r => r.Id == dto.RecurrenceTypeId);

                if (!recurrenceTypeExists)
                {
                    return Result<RecurringExpenseDto>.Fail("Invalid recurrence type");
                }

                // Controllo PaymentType
                var paymentTypeExists = await _dbContext.Paymenttypes
                    .AnyAsync(p => p.Id == dto.PaymentTypeId);

                if (!paymentTypeExists)
                {
                    return Result<RecurringExpenseDto>.Fail("Invalid payment type");
                }

                // UPDATE FIELDS
                entity.Recurrencetypeid = dto.RecurrenceTypeId;
                entity.Paymenttypeid = dto.PaymentTypeId;
                entity.Date = dto.Date;
                entity.Automatic = dto.Automatic;
                entity.Amount = dto.Amount;
                entity.Description = dto.Description;

                await _dbContext.SaveChangesAsync();

                // ritorno DTO aggiornato
                return Result<RecurringExpenseDto>.Ok(new RecurringExpenseDto
                {
                    Id = entity.Id,
                    Amount = entity.Amount,
                    Description = entity.Description,
                    RecurrenceTypeId = entity.Recurrencetypeid,
                    PaymentTypeId = entity.Paymenttypeid,
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
                return Result<RecurringExpenseDto>.Fail(errorMsg);
            }
        }


        // =========================
        // DELETE
        // =========================
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbContext.Recurringexpenses.FindAsync(id);

                if (entity == null)
                {
                    return Result<bool>.Fail("Recurring expense not found");
                }

                _dbContext.Recurringexpenses.Remove(entity);
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
