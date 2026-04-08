namespace CycleMoney.Shared.Converter
{
    public static class PaymentTypeConverter
    {
        public static PaymentType ToEntity(PaymentTypeDto dto)
        {
            return new PaymentType
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static PaymentTypeDto ToDto(PaymentType entity)
        {
            return new PaymentTypeDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
