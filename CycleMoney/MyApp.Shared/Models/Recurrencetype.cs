using System;
using System.Collections.Generic;

namespace CycleMoney.Shared.Models;

public partial class Recurrencetype
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Recurringexpense> Recurringexpenses { get; set; } = new List<Recurringexpense>();
}
