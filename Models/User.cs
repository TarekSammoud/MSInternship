using System;
using System.Collections.Generic;

namespace MSS.API.Models;

public partial class User
{
    public string NumTel { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Cin { get; set; } = null!;

    public string? Address { get; set; }

    public DateTime BirthDay { get; set; }

    public string? Mf { get; set; }

    public string? Mcc { get; set; }

    public string? WalletId { get; set; }

    public string? Roles { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public string Password { get; set; } = null!;
}
