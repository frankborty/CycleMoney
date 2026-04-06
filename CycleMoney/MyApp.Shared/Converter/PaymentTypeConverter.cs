namespace CycleMoney.Shared.Converter
{
    public static class PaymentTypeConverter
    {
        public static Paymenttype ToEntity(PaymentTypeDto dto)
        {
            return new Paymenttype
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static PaymentTypeDto ToDto(Paymenttype entity)
        {
            return new PaymentTypeDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
