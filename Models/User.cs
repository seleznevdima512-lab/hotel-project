using System;
using System.Collections.Generic;

namespace hotel.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public sbyte IsBlocked { get; set; }

    public int FailedAttempts { get; set; }
}
