namespace MyApp.Shared.DTOs
{
    public class CreateRecurringExpenseDto
    {
        public decimal Amount { get; set; }
        public int RecurrenceTypeId { get; set; }
        public DateOnly Date { get; set; }
        public string? Description { get; set; }
        public int PaymentTypeId { get; set; }
        public bool Automatic { get; set; }
    }
}
