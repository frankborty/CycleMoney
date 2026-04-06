namespace CycleMoney.API.Services.Interfaces
{
    public interface IRecurrenceTypeService
    {
        Task<Result<List<RecurrenceTypeDto>>> GetAllAsync();
        Task<Result<RecurrenceTypeDto>> GetByIdAsync(int id);
        Task<Result<RecurrenceTypeDto>> CreateAsync(string recurrenceTypeName);
        Task<Result<RecurrenceTypeDto>> UpdateAsync(int id, string recurrenceTypeName);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
