namespace CycleMoney.Shared.DTOs
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public decimal? Amount { get; set; }
        public int RecurrenceTypeId { get; set; }
        public string RecurrenceTypeName { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public string? Description { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; } = string.Empty;
        public bool Automatic { get; set; }
        public bool FixedPrice { get; set; }
    }
}
