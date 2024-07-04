using System;
using System.Collections.Generic;

namespace MSS.API.Models;

public partial class Bank
{
    public Guid BankCode { get; set; }

    public string BankName { get; set; } = null!;

    public string? NumTel { get; set; }

    public int? NbUsers { get; set; }

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
