using System;
using System.Collections.Generic;

namespace MSS.API.Models;

public partial class Transaction
{
    public Guid? Id { get; set; }

    public string? Source { get; set; }

    public string? Destination { get; set; }

    public DateTime? TransactionDate { get; set; }
}
