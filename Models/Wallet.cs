using System;
using System.Collections.Generic;

namespace MSS.API.Models;

public partial class Wallet
{
    public Guid WalletId { get; set; }

    public string User { get; set; } = null!;

    public DateTime? Validity { get; set; }

    public Guid? BankCode { get; set; }

    public bool Status { get; set; }

    public decimal? Solde { get; set; }

    public virtual Bank? BankCodeNavigation { get; set; }
}
