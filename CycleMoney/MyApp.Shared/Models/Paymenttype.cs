using System;
using System.Collections.Generic;

namespace MyApp.Shared.Models;

public partial class Paymenttype
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Recurringexpense> Recurringexpenses { get; set; } = new List<Recurringexpense>();
}
