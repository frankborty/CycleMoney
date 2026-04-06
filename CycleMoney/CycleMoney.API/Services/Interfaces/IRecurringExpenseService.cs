namespace CycleMoney.API.Services.Interfaces
{
    public interface IRecurringExpenseService
    {
        Task<Result<List<RecurringExpenseDto>>> GetAllAsync();
        Task<Result<RecurringExpenseDto>> GetByIdAsync(int id);
        Task<Result<RecurringExpenseDto>> CreateAsync(RecurringExpenseDto dto);
        Task<Result<RecurringExpenseDto>> UpdateAsync(int id, RecurringExpenseDto dto);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
