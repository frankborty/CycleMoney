namespace CycleMoney.FrontEnd.Models
{
    public class ExpenseFilters
    {
        public List<string> PaymentTypes { get; set; } = [];
        public List<string> Recurrencies { get; set; } = [];
        public string? Search { get; set; }
    }
}
