namespace CycleMoney.API.Services.Interfaces
{
    public interface IRecurringExpenseService
    {
        Task<Result<List<ExpenseDto>>> GetAllAsync();
        Task<Result<ExpenseDto>> GetByIdAsync(int id);
        Task<Result<ExpenseDto>> CreateAsync(ExpenseDto dto);
        Task<Result<ExpenseDto>> UpdateAsync(int id, ExpenseDto dto);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
