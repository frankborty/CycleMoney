namespace CycleMoney.Shared.Converter
{
    public static class RecurrenceTypeConverter
    {
        public static RecurrenceType ToEntity(RecurrenceTypeDto dto)
        {
            return new RecurrenceType
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static RecurrenceTypeDto ToDto(RecurrenceType entity)
        {
            return new RecurrenceTypeDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
