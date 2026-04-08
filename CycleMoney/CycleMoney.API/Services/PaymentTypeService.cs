namespace CycleMoney.API.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly CycleMoneyRemoteDbContext _dbContext;

        public PaymentTypeService(CycleMoneyRemoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<Result<List<PaymentTypeDto>>> GetAllAsync()
        {
            try
            {
                List<PaymentType> data = await _dbContext.PaymentTypes.ToListAsync();

                var result = data.Select(r => PaymentTypeConverter.ToDto(r)).ToList();
                return Result<List<PaymentTypeDto>>.Ok(result);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException is not null)
                {
                    errorMsg += " - InnerExc: " + ex.InnerException;
                }
                return Result<List<PaymentTypeDto>>.Fail(errorMsg);
            }
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<Result<PaymentTypeDto>> GetByIdAsync(int id)
        {
            PaymentType? paymentoType = await _dbContext.PaymentTypes.FindAsync(id);
            return paymentoType is null
                ? Result<PaymentTypeDto>.Fail("PaymentType not found")
                : Result<PaymentTypeDto>.Ok(PaymentTypeConverter.ToDto(paymentoType));
        }

        // =========================
        // CREATE
        // =========================
        public async Task<Result<PaymentTypeDto>> CreateAsync(string paymentTypeName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(paymentTypeName))
                {
                    return Result<PaymentTypeDto>.Fail("PaymentType must have a name");
                }

                var paymentType = await _dbContext.PaymentTypes
                    .FirstOrDefaultAsync(r => r.Name == paymentTypeName);

                if (paymentType is not null)
                {
                    return Result<PaymentTypeDto>.Fail($"A PaymentType with the same name already exists {paymentTypeName}");
                }

                PaymentType entity = new PaymentType
                {
                    Name = paymentTypeName
                };
                _dbContext.PaymentTypes.Add(entity);
                await _dbContext.SaveChangesAsync();
                return Result<PaymentTypeDto>.Ok(PaymentTypeConverter.ToDto(entity));
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException is not null)
                {
                    errorMsg += " - InnerExc: " + ex.InnerException;
                }
                return Result<PaymentTypeDto>.Fail(errorMsg);
            }
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<Result<PaymentTypeDto>> UpdateAsync(int id, string paymentTypeName)
        {
            try
            {
                var entity = await _dbContext.PaymentTypes.FindAsync(id);

                if (entity == null)
                {
                    return Result<PaymentTypeDto>.Fail("PaymentType not found");
                }

                if (string.IsNullOrWhiteSpace(paymentTypeName))
                {
                    return Result<PaymentTypeDto>.Fail("PaymentType must have a name");
                }

                // UPDATE FIELDS
                entity.Name = paymentTypeName;
                await _dbContext.SaveChangesAsync();

                // ritorno  aggiornato
                return Result<PaymentTypeDto>.Ok(PaymentTypeConverter.ToDto(entity));
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException is not null)
                {
                    errorMsg += " - InnerExc: " + ex.InnerException;
                }
                return Result<PaymentTypeDto>.Fail(errorMsg);
            }
        }


        // =========================
        // DELETE
        // =========================
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbContext.PaymentTypes.FindAsync(id);

                if (entity == null)
                {
                    return Result<bool>.Fail("PaymentType not found");
                }

                _dbContext.PaymentTypes.Remove(entity);
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
                return Result<bool>.Fail($"Error deleting paymentType: {errorMsg}");
            }
        }
    }
}
