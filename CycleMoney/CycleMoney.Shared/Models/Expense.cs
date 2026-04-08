namespace CycleMoney.Shared.Models;

public partial class Expense
{
    public int Id { get; set; }

    public decimal? Amount { get; set; }

    public int RecurrenceTypeId { get; set; }

    public DateOnly Date { get; set; }

    public string? Description { get; set; }

    public int PaymentTypeId { get; set; }

    public bool Automatic { get; set; }

    public bool FixedPrice { get; set; }

    public virtual PaymentType PaymentType { get; set; } = null!;

    public virtual RecurrenceType RecurrenceType { get; set; } = null!;
}
