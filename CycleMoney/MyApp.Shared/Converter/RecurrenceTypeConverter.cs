namespace CycleMoney.Shared.Converter
{
    public static class RecurrenceTypeConverter
    {
        public static Recurrencetype ToEntity(RecurrenceTypeDto dto)
        {
            return new Recurrencetype
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static RecurrenceTypeDto ToDto(Recurrencetype entity)
        {
            return new RecurrenceTypeDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
