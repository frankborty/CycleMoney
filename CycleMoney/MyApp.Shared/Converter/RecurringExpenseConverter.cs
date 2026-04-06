namespace CycleMoney.Shared.Converter
{
    public static class RecurringExpenseConverter
    {
        public static Recurringexpense ToEntity(RecurringExpenseDto dto)
        {
            return new Recurringexpense
            {
                Id = dto.Id,
                Amount = dto.Amount,
                Description = dto.Description,
                Recurrencetypeid = dto.RecurrenceTypeId,
                Date = dto.Date,
                Paymenttypeid = dto.PaymentTypeId,
                Automatic = dto.Automatic
            };
        }

        public static RecurringExpenseDto ToDto(Recurringexpense entity)
        {
            return new RecurringExpenseDto
            {
                Id = entity.Id,
                Amount = entity.Amount,
                Description = entity.Description,
                RecurrenceTypeId = entity.Recurrencetype.Id,
                RecurrenceTypeName = entity.Recurrencetype.Name,
                Date = entity.Date,
                PaymentTypeId = entity.Paymenttype.Id,
                PaymentTypeName = entity.Paymenttype.Name,
                Automatic = entity.Automatic
            };
        }
    }
}
