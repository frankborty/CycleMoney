namespace CycleMoney.API.Services
{
    public class RecurrenceTypeService : IRecurrenceTypeService
    {
        private readonly CycleMoneyRemoteDbContext _dbContext;

        public RecurrenceTypeService(CycleMoneyRemoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<Result<List<RecurrenceTypeDto>>> GetAllAsync()
        {
            try
            {
                List<RecurrenceType> data = await _dbContext.RecurrenceTypes.ToListAsync();
                var result = data.Select(r => RecurrenceTypeConverter.ToDto(r)).ToList();
                return Result<List<RecurrenceTypeDto>>.Ok(result);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException is not null)
                {
                    errorMsg += " - InnerExc: " + ex.InnerException;
                }
                return Result<List<RecurrenceTypeDto>>.Fail(errorMsg);
            }
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<Result<RecurrenceTypeDto>> GetByIdAsync(int id)
        {
            RecurrenceType? recurrenceType = await _dbContext.RecurrenceTypes.FindAsync(id);
            return recurrenceType is null
                ? Result<RecurrenceTypeDto>.Fail("RecurrenceType not found")
                : Result<RecurrenceTypeDto>.Ok(RecurrenceTypeConverter.ToDto(recurrenceType));
        }

        // =========================
        // CREATE
        // =========================
        public async Task<Result<RecurrenceTypeDto>> CreateAsync(string RecurrenceTypeName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(RecurrenceTypeName))
                {
                    return Result<RecurrenceTypeDto>.Fail("RecurrenceType must have a name");
                }

                var RecurrenceType = await _dbContext.RecurrenceTypes
                    .FirstOrDefaultAsync(r => r.Name == RecurrenceTypeName);

                if (RecurrenceType is not null)
                {
                    return Result<RecurrenceTypeDto>.Fail($"A RecurrenceType with the same name already exists {RecurrenceTypeName}");
                }

                RecurrenceType entity = new RecurrenceType
                {
                    Name = RecurrenceTypeName
                };
                _dbContext.RecurrenceTypes.Add(entity);
                await _dbContext.SaveChangesAsync();
                return Result<RecurrenceTypeDto>.Ok(RecurrenceTypeConverter.ToDto(entity));
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException is not null)
                {
                    errorMsg += " - InnerExc: " + ex.InnerException;
                }
                return Result<RecurrenceTypeDto>.Fail(errorMsg);
            }
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<Result<RecurrenceTypeDto>> UpdateAsync(int id, string RecurrenceTypeName)
        {
            try
            {
                var entity = await _dbContext.RecurrenceTypes.FindAsync(id);

                if (entity == null)
                {
                    return Result<RecurrenceTypeDto>.Fail("RecurrenceType not found");
                }

                if (string.IsNullOrWhiteSpace(RecurrenceTypeName))
                {
                    return Result<RecurrenceTypeDto>.Fail("RecurrenceType must have a name");
                }

                // UPDATE FIELDS
                entity.Name = RecurrenceTypeName;
                await _dbContext.SaveChangesAsync();

                // ritorno  aggiornato
                return Result<RecurrenceTypeDto>.Ok(RecurrenceTypeConverter.ToDto(entity));
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                if (ex.InnerException is not null)
                {
                    errorMsg += " - InnerExc: " + ex.InnerException;
                }
                return Result<RecurrenceTypeDto>.Fail(errorMsg);
            }
        }


        // =========================
        // DELETE
        // =========================
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var entity = await _dbContext.RecurrenceTypes.FindAsync(id);

                if (entity == null)
                {
                    return Result<bool>.Fail("RecurrenceType not found");
                }

                _dbContext.RecurrenceTypes.Remove(entity);
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
                return Result<bool>.Fail($"Error deleting RecurrenceType: {errorMsg}");
            }
        }
    }
}
