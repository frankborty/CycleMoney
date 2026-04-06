namespace CycleMoney.Shared.Models;

public partial class Recurringexpense
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public int Recurrencetypeid { get; set; }

    public DateOnly Date { get; set; }

    public string? Description { get; set; }

    public int Paymenttypeid { get; set; }

    public bool Automatic { get; set; }

    public virtual Paymenttype Paymenttype { get; set; } = null!;

    public virtual Recurrencetype Recurrencetype { get; set; } = null!;
}
