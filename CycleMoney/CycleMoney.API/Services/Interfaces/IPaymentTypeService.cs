namespace CycleMoney.API.Services.Interfaces
{
    public interface IPaymentTypeService
    {
        Task<Result<List<PaymentTypeDto>>> GetAllAsync();
        Task<Result<PaymentTypeDto>> GetByIdAsync(int id);
        Task<Result<PaymentTypeDto>> CreateAsync(string paymentTypeName);
        Task<Result<PaymentTypeDto>> UpdateAsync(int id, string paymentTypeName);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
