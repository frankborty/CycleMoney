namespace CycleMoney.Shared.Converter
{
    public static class ExpenseConverter
    {
        public static Expense ToEntity(ExpenseDto dto)
        {
            return new Expense
            {
                Id = dto.Id,
                Amount = dto.Amount,
                Description = dto.Description,
                RecurrenceTypeId = dto.RecurrenceTypeId,
                Date = dto.Date,
                PaymentTypeId = dto.PaymentTypeId,
                Automatic = dto.Automatic,
                FixedPrice = dto.FixedPrice
            };
        }

        public static ExpenseDto ToDto(Expense entity)
        {
            return new ExpenseDto
            {
                Id = entity.Id,
                Amount = entity.Amount,
                Description = entity.Description,
                RecurrenceTypeId = entity.RecurrenceType.Id,
                RecurrenceTypeName = entity.RecurrenceType.Name,
                Date = entity.Date,
                PaymentTypeId = entity.PaymentType.Id,
                PaymentTypeName = entity.PaymentType.Name,
                Automatic = entity.Automatic,
                FixedPrice = entity.FixedPrice
            };
        }
    }
}
